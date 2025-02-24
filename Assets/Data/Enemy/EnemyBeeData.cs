using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBeeData", menuName = "Scriptable Objects/EnemyBeeData")]
public class EnemyBeeData : EnemyData
{
    [Header("Bee")]
    [Tooltip("������ ����")]
    public int platePower;

    [Tooltip("������ ����")]
    public float plateRange;

    [Tooltip("������ �ߵ� �ֱ�")]
    public float platePeriod;
}