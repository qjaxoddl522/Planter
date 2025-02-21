using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    const float spawnXpos = 11;
    const float spawnYRange = 2;

    [SerializeField] OtherSystemData sysData;
    [SerializeField] List<WaveData> waves;

    public event Action OnEnemyEliminated;
    int enemyCount = 0;

    public void WaveStart(int day)
    {
        SpawnWave(waves[day-1]);
    }

    void SpawnWave(WaveData wave)
    {
        foreach (SpawnData spawn in wave.spawnData)
        {
            //Debug.Log("Spawn " + spawn.enemy.enemyPrefab.name + " x" + spawn.amount);
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
            Vector2 spawnPos = new Vector2(0, 0);
            switch (spawn.xPos)
            {
                case XPos.Left:
                    spawnPos = new Vector2(-spawnXpos, Random.Range(-spawnYRange / 2, spawnYRange / 2));
                    break;
                case XPos.Right:
                    spawnPos = new Vector2(spawnXpos, Random.Range(-spawnYRange / 2, spawnYRange / 2));
                    break;
                case XPos.Both:
                    spawnPos = new Vector2(Random.Range(0, 2) == 0 ? -spawnXpos : spawnXpos, Random.Range(-spawnYRange / 2, spawnYRange / 2));
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
            enemyCount++;

            yield return new WaitForSeconds(spawnInterval + Random.Range(-(nightTime * randomRate), nightTime * randomRate));
        }
    }
}
