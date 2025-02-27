using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimeViewSlider : MonoBehaviour, IHidedUI
{
    [SerializeField] OtherSystemData sysData;
    [SerializeField] GameObject sliderHandle;
    Slider slider;
    Vector3 initPos;

    public void InitPosision(float dist)
    {
        initPos = transform.position;
        transform.position += new Vector3(0, dist, 0);
    }

    public void SlideIn(float duration)
    {
        transform.DOMove(initPos, duration).SetEase(Ease.OutCubic);
    }

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
