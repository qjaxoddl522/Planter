using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData[] enemyData;

    void Start()
    {
        //var inst = Instantiate(enemyData[2].enemyPrefab, new Vector3(-10, 0), Quaternion.identity);
        //inst.GetComponent<EnemyBase>().enemyData = enemyData[0];
        var inst = Instantiate(enemyData[3].enemyPrefab, new Vector3(10, 0), Quaternion.identity);
        inst.GetComponent<EnemyBase>().enemyData = enemyData[1];
    }
}
