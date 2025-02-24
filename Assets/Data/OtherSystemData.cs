using UnityEngine;

[CreateAssetMenu(fileName = "OtherSystemData", menuName = "Scriptable Objects/OtherSystemData", order = 51)]
public class OtherSystemData : ScriptableObject
{
    [Tooltip("���� �� �Ϸ��� ���� �ð�(��)")]
    public int maxDayTime;

    [Tooltip("�ʱ� �ð�(0.5 = ��)")]
    public float initTime;

    [Tooltip("�ʱ� ����")]
    public int initCoin;

    [Tooltip("�ʱ� �� ���� ��")]
    public int initSpotLength;

    [Tooltip("�ִ� �� ���� ��")]
    public int maxSpotLength;

    [Tooltip("�� �� �� Ȯ�� ����")]
    public int spotExtendPrice;

    [Tooltip("����� ����")]
    public int fertilizerPrice;
}
