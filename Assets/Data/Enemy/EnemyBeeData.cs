using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBeeData", menuName = "Scriptable Objects/EnemyBeeData")]
public class EnemyBeeData : EnemyData
{
    [Header("Bee")]
    [Tooltip("장판의 위력")]
    public int platePower;

    [Tooltip("장판의 범위")]
    public float plateRange;

    [Tooltip("장판의 발동 주기")]
    public float platePeriod;
}