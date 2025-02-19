using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData[] enemyData;

    void Start()
    {
        CreateEnemy(Enemy.DungBeetle);
        CreateEnemy(Enemy.Earthworm);
    }

    void CreateEnemy(Enemy enemy)
    {
        var inst = Instantiate(enemyData[(int)enemy].enemyPrefab, new Vector3(10, Random.Range(-2f, 2f)), Quaternion.identity);
        inst.GetComponent<EnemyBase>().enemyData = enemyData[(int)enemy];
    }
}
