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

    public void ShakeIcon()
    {
        vTimeSunMoon.ShakeIcon();
    }

    void DayTimeUpdate()
    {
        vTimeSlider.UpdateSlider(mTime.GameTime);
    }

    void DayUpdate()
    {
        if (waveSpawner.IsWaveEnd(mTime.Day))
        {
            Debug.Log("Game Clear!");
            mTime.IsGamePause = true;
            vTimeSlider.HideSliderHandle();
            return;
        }

        vTimeSunMoon.UpdateIcon(true);
        vTimeDark.UpdateDarkness(true);
        waveSpawner.InjectWaveInfo(mTime.Day);
        isDaytime = true;
        OnDaytimeChanged?.Invoke();

        AudioManager.Instance.PlaySFX(AudioManager.SFX.DayComplete);
    }

    void NightUpdate()
    {
        vTimeSunMoon.UpdateIcon(false);
        vTimeDark.UpdateDarkness(false);
        waveSpawner.WaveStart(mTime.Day);
        waveSpawner.DestroyEnemyInfo();
        isDaytime = false;
        OnNightChanged?.Invoke();

        AudioManager.Instance.PlaySFX(AudioManager.SFX.NightStart);
    }
}
