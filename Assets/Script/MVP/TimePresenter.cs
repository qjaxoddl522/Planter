using System;
using System.Collections;
using TMPro;
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
    [SerializeField] TextMeshProUGUI dayText;

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

    public IEnumerator DayStart()
    {
        isDaytime = true;
        yield return new WaitForSeconds(2f);
        mTime.DayStart();
    }

    void DayTimeUpdate()
    {
        vTimeSlider.UpdateSlider(mTime.GameTime);
    }

    void DayUpdate()
    {
        vTimeSunMoon.UpdateIcon(true);
        vTimeDark.UpdateDarkness(true);
        dayText.text = "Day " + mTime.Day;

        if (waveSpawner.IsWaveEnd(mTime.Day))
        {
            mTime.IsGamePause = true;
            vTimeSlider.HideSliderHandle();

            StartCoroutine(GameProcessManager.Instance.GameClear());
            return;
        }
        
        waveSpawner.InjectWaveInfo(mTime.Day);
        isDaytime = true;

        if (mTime.Day > 1)
            AudioManager.Instance.PlaySFX(AudioManager.SFX.DayComplete);
    }

    void NightUpdate()
    {
        vTimeSunMoon.UpdateIcon(false);
        vTimeDark.UpdateDarkness(false);
        waveSpawner.WaveStart();
        waveSpawner.DestroyEnemyInfo();
        isDaytime = false;
        OnNightChanged?.Invoke();

        AudioManager.Instance.PlaySFX(AudioManager.SFX.NightStart);
    }
}
