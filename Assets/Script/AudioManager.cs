using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    [Header("BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    AudioSource bgmPlayer;

    public enum BGM
    {
        DayAndNight,
        Night,
    }

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum SFX
    {
        ShootPlant,
        ShootSpreadPlant,
        HitPlant,
        HitEnemy,
        Pop,
        DayComplete,
        NightStart,
        Weep,
        EarnCoin,
        CashMachine,
        Explosion,
        GameOver,
    }

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        // 배경음
        GameObject bgmObject = new GameObject("BGMPlayer");
        bgmObject.transform.SetParent(transform);
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        

        // 효과음
        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.SetParent(transform);
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < channels; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }
    }

    public void PlayBGM(BGM bgm)
    {
        bgmPlayer.clip = bgmClips[(int)bgm];
        bgmPlayer.Play();
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(SFX sfx)
    {
        if (!GameProcessManager.Instance.isGameEnd || sfx == SFX.GameOver || sfx == SFX.DayComplete)
        {
            for (int i = 0; i < channels; i++)
            {
                int loopIndex = (channelIndex + i) % channels;
                if (!sfxPlayers[loopIndex].isPlaying)
                {
                    channelIndex = loopIndex;
                    sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
                    sfxPlayers[loopIndex].Play();
                    break;
                }
            }
        }
    }
}
