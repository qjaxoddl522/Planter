using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveInfoManager : MonoBehaviour
{
    [SerializeField] GameObject waveInfoPrefab;
    [SerializeField] float xPos;
    [SerializeField] float ySpacing;
    [SerializeField] float xNoise;
    [SerializeField] float yNoise;
    [SerializeField] float minScale;

    public List<(EnemyData enemy, int amount)> leftEnemies;
    public List<(EnemyData enemy, int amount)> rightEnemies;
    List<GameObject> enemyInfos;

    void Awake()
    {
        leftEnemies = new List<(EnemyData enemy, int amount)>();
        rightEnemies = new List<(EnemyData enemy, int amount)>();
        enemyInfos = new List<GameObject>();
    }

    void Init()
    {
        leftEnemies.Clear();
        rightEnemies.Clear();
        enemyInfos.Clear();
    }

    public void CreateEnemyInfo()
    {
        CreateInfo(leftEnemies, false);
        CreateInfo(rightEnemies, true);
    }

    void CreateInfo(List<(EnemyData enemy, int amount)> enemies, bool isRight)
    {
        if (enemies.Count > 0)
        {
            int maxAmount = enemies.Max(e => e.amount);

            enemies.Shuffle();
            for (int i = 0; i < enemies.Count; i++)
            {
                float offset = (i - (enemies.Count - 1) * 0.5f) * ySpacing;
                float yPos = offset + Random.Range(-yNoise, yNoise);
                var inst = Instantiate(waveInfoPrefab,
                    new Vector2(xPos * (isRight ? 1 : -1) + Random.Range(-xNoise, xNoise), offset), Quaternion.identity);
                enemyInfos.Add(inst);
                var info = inst.GetComponent<WaveInfo>();
                info.Init(enemies[i].enemy, minScale + ((float)enemies[i].amount / maxAmount) * (1 - minScale), isRight);
            }
        }
    }

    public void DestroyEnemyInfo()
    {
        foreach (var info in enemyInfos)
        {
            Destroy(info.gameObject);
        }
        Init();
    }
}
