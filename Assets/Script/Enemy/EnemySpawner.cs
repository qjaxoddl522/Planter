using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData[] enemyData;

    void Start()
    {
        CreateEnemy(Enemy.DungBeetle);
    }

    void CreateEnemy(Enemy enemy)
    {
        var inst = Instantiate(enemyData[(int)enemy].enemyPrefab, new Vector3(10, 0), Quaternion.identity);
        inst.GetComponent<EnemyBase>().enemyData = enemyData[(int)enemy];
    }
}
