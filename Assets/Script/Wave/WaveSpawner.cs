using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class WaveListWrapper
{
    public List<WaveData> waves;
}

public class WaveSpawner : MonoBehaviour
{
    const float spawnXpos = 10;

    [SerializeField] OtherSystemData sysData;
    [SerializeField] List<WaveListWrapper> waveWrappers;

    public event Action OnEnemyEliminated;
    WaveData nextWave;
    int enemyCount = 0;
    WaveInfoManager waveInfoManager;
    StaticsManager staticsManager;

    void Awake()
    {
        waveInfoManager = GetComponent<WaveInfoManager>();
        staticsManager = GetComponent<StaticsManager>();
    }

    public void InjectWaveInfo(int day)
    {
        int index = Random.Range(0, waveWrappers[day - 1].waves.Count);
        nextWave = waveWrappers[day - 1].waves[index];
        foreach (SpawnData spawn in nextWave.spawnData)
        {
            if (spawn.xPos == XPos.Left)
                waveInfoManager.leftEnemies.Add((spawn.enemy, spawn.amount));
            else if (spawn.xPos == XPos.Right)
                waveInfoManager.rightEnemies.Add((spawn.enemy, spawn.amount));
            else
            {
                waveInfoManager.leftEnemies.Add((spawn.enemy, (int)(spawn.amount / 1.5f)));
                waveInfoManager.rightEnemies.Add((spawn.enemy, (int)(spawn.amount / 1.5f)));
            }
        }
        waveInfoManager.CreateEnemyInfo();
    }

    public void DestroyEnemyInfo() { waveInfoManager.DestroyEnemyInfo(); }

    public bool IsWaveEnd(int day)
    {
        return waveWrappers.Count < day;
    }

    public void WaveStart()
    {
        SpawnWave(nextWave);
    }

    void SpawnWave(WaveData wave)
    {
        foreach (SpawnData spawn in wave.spawnData)
        {
            staticsManager.enemyCount[(int)spawn.enemy.enemyID] += spawn.amount;
            enemyCount += spawn.amount;
            StartCoroutine(SpawnEnemies(spawn));
        }
    }

    IEnumerator SpawnEnemies(SpawnData spawn)
    {
        float randomRate = 0.1f;
        float nightTime = sysData.maxDayTime / 2;
        float spawnInterval = (nightTime * 4 / 5) / spawn.amount;
        
        for (int i = 0; i < spawn.amount; i++)
        {
            float yPos = Random.Range(-1, 2);
            Vector2 spawnPos = new Vector2(0, yPos + Random.Range(0, 0.4f));

            switch (spawn.xPos)
            {
                case XPos.Left:
                    spawnPos += new Vector2(-spawnXpos, 0);
                    break;
                case XPos.Right:
                    spawnPos += new Vector2(spawnXpos, 0);
                    break;
                case XPos.Both:
                    spawnPos += new Vector2(Random.Range(0, 2) == 0 ? -spawnXpos : spawnXpos, 0);
                    break;
            }

            var inst = Instantiate(spawn.enemy.enemyPrefab, spawnPos, Quaternion.identity);
            var enemy = inst.GetComponent<EnemyBase>();
            enemy.enemyData = spawn.enemy;
            enemy.OnDeath += () => {
                enemyCount--;
                if (enemyCount == 0)
                    OnEnemyEliminated?.Invoke();
            };

            yield return new WaitForSeconds(spawnInterval + Random.Range(-(nightTime * randomRate), nightTime * randomRate / 2));
        }
    }
}
