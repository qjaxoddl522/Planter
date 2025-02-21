using System;
using UnityEngine;

public class TimePresenter : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] TimeModel mTime;

    [Header("View")]
    [SerializeField] TimeViewSlider vTimeSlider;
    [SerializeField] TimeViewSunMoon vTimeSunMoon;
    [SerializeField] TimeViewDark vTimeDark;
    [SerializeField] WaveSpawner waveSpawner;

    public static Action OnDaytimeChanged;
    public static Action OnNightChanged;
    public static bool isDaytime;

    void Start()
    {
        mTime.OnTimeChanged += DayTimeUpdate;
        mTime.OnDaytimeChanged += DayUpdate;
        mTime.OnNightChanged += NightUpdate;
    }

    void DayTimeUpdate()
    {
        vTimeSlider.UpdateSlider(mTime.GameTime);
    }

    void DayUpdate()
    {
        vTimeSunMoon.UpdateIcon(true);
        vTimeDark.UpdateDarkness(true);
        isDaytime = true;
        OnDaytimeChanged?.Invoke();
    }

    void NightUpdate()
    {
        vTimeSunMoon.UpdateIcon(false);
        vTimeDark.UpdateDarkness(false);
        waveSpawner.WaveStart(mTime.Day);
        isDaytime = false;
        OnNightChanged?.Invoke();
    }
}
