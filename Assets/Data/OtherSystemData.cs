using UnityEngine;

[CreateAssetMenu(fileName = "OtherSystemData", menuName = "Scriptable Objects/OtherSystemData", order = 51)]
public class OtherSystemData : ScriptableObject
{
    [Tooltip("게임 상 하루의 실제 시간(초)")]
    public int maxDayTime;

    [Tooltip("초기 시간(0.5 = 밤)")]
    public float initTime;

    [Tooltip("초기 코인")]
    public int initCoin;

    [Tooltip("초기 밭 열의 수")]
    public int initSpotLength;

    [Tooltip("최대 밭 열의 수")]
    public int maxSpotLength;

    [Tooltip("열 당 밭 확장 가격")]
    public int spotExtendPrice;

    [Tooltip("비료의 가격")]
    public int fertilizerPrice;
}
