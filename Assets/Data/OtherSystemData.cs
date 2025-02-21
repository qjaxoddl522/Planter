using UnityEngine;

[CreateAssetMenu(fileName = "OtherSystemData", menuName = "Scriptable Objects/OtherSystemData", order = 51)]
public class OtherSystemData : ScriptableObject
{
    [Tooltip("���� �� �Ϸ��� ���� �ð�(��)")]
    public int maxDayTime;

    [Tooltip("�ʱ� ����")]
    public int initCoin;

    [Tooltip("����� ����")]
    public int fertilizerPrice;
}
