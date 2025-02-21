using UnityEngine;
using UnityEngine.UI;

public class TimeViewSlider : MonoBehaviour
{
    [SerializeField] OtherSystemData sysData;
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        slider.maxValue = sysData.maxDayTime;
    }

    public void UpdateSlider(float value)
    {
        slider.value = value;
    }
}
