using UnityEngine;
using UnityEngine.UI;

public class TimeViewSlider : MonoBehaviour
{
    [SerializeField] OtherSystemData sysData;
    [SerializeField] GameObject sliderHandle;
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

    public void HideSliderHandle()
    {
        sliderHandle.SetActive(false);
    }
}
