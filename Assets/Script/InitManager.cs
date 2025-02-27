using UnityEngine;

public interface IManager
{
    void Init();
}

public class InitManager : MonoBehaviour
{
    SystemLoader loader;

    void Awake()
    {
        Application.targetFrameRate = 60;
        loader = SystemLoader.Instance;
    }

    void Start()
    {
        loader.InitAll();
        AudioManager.Instance.PlayBGM(AudioManager.BGM.DayAndNight);
    }
}
