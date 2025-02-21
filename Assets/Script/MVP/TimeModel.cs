using System;
using UnityEngine;

public class TimeModel : MonoBehaviour
{
    [SerializeField] OtherSystemData sysData;
    [SerializeField] WaveSpawner waveSpawner;

    public event Action OnTimeChanged; // 시간 변화
    public event Action OnDaytimeChanged;  // 낮(날짜) 변화
    public event Action OnNightChanged; // 밤 변화
    
    bool isEnemyEliminated;

    [SerializeField] float gameTime;
    public float GameTime
    {
        get { return gameTime; }
        private set
        {
            gameTime = value;
            OnTimeChanged?.Invoke();
        }
    }

    [SerializeField] int day;
    public int Day
    {
        get { return day; }
        private set
        {
            day = value;
            OnDaytimeChanged?.Invoke();
        }
    }

    [SerializeField] bool isDaytime;
    public bool IsDaytime
    {
        get { return isDaytime; }
        private set { isDaytime = value; }
    }

    void Start()
    {
        isEnemyEliminated = true;
        
        GameTime = 0;
        Day = 1;
        IsDaytime = true;

        waveSpawner.OnEnemyEliminated += () => isEnemyEliminated = true;
    }

    void Update()
    {
        if (IsDaytime && GameTime >= sysData.maxDayTime / 2.0f)
        {
            IsDaytime = false;
            isEnemyEliminated = false;
            OnNightChanged?.Invoke();
        }

        if (GameTime >= sysData.maxDayTime && isEnemyEliminated)
        {
            GameTime = 0;
            IsDaytime = true;
            Day++;
        }
        else
        {
            GameTime = Mathf.Min(sysData.maxDayTime, GameTime + Time.deltaTime);
        }
    }
}
