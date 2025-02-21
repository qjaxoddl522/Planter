using UnityEngine;

[CreateAssetMenu(fileName = "OtherSystemData", menuName = "Scriptable Objects/OtherSystemData", order = 51)]
public class OtherSystemData : ScriptableObject
{
    [Tooltip("게임 상 하루의 실제 시간(초)")]
    public int maxDayTime;

    [Tooltip("초기 코인")]
    public int initCoin;

    [Tooltip("비료의 가격")]
    public int fertilizerPrice;
}
