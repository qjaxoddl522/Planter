using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData[] enemyData;

    void Start()
    {
        var inst = Instantiate(enemyData[0].enemyPrefab, transform.position, Quaternion.identity);
        inst.GetComponent<EnemyBase>().enemyData = enemyData[0];
    }
}
