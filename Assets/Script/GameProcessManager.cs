using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface IHidedUI
{
    void InitPosision(float dist);
    void SlideIn(float duration);
}

public class GameProcessManager : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] hidedUI;
    [SerializeField] Transform[] needToHide;

    [SerializeField] TimePresenter timePresenter;
    [SerializeField] Transform failText;
    [SerializeField] Image fadeImage;

    [SerializeField] GameObject clearUI;
    [SerializeField] GamespeedButton gamespeedButton;
    StaticsManager staticsManager;

    static GameProcessManager instance;
    public static GameProcessManager Instance { get { return instance; } }

    public bool isGameStart { private set; get; }
    public bool isGameEnd { private set; get; }

    void Awake()
    {
        instance = this;
        staticsManager = GetComponent<StaticsManager>();
    }

    void Start()
    {
        foreach (var hidedUI in hidedUI)
        {
            var iHidedUI = hidedUI as IHidedUI;
            iHidedUI.InitPosision(2.5f);
        }
    }

    public void GameStart()
    {
        isGameStart = true;
        foreach (var hidedUI in hidedUI)
        {
            var iHidedUI = hidedUI as IHidedUI;
            iHidedUI.SlideIn(1.5f);
        }

        foreach (var UI in needToHide)
        {
            UI.gameObject.SetActive(false);
        }

        StartCoroutine(timePresenter.DayStart());
    }

    public IEnumerator GameClear()
    {
        isGameEnd = true;
        gamespeedButton.ChangeGamespeed(1.0f);

        yield return new WaitForSeconds(2f);

        staticsManager.UpdateText();
        clearUI.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.SFX.DayComplete);
    }

    public void GameEnd()
    {
        isGameEnd = true;
        gamespeedButton.ChangeGamespeed(1.0f);

        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlaySFX(AudioManager.SFX.GameOver);

        failText.DOMoveY(2.5f, 3f).SetEase(Ease.OutBounce).OnComplete(() => {
            DOVirtual.DelayedCall(1f, FadeOut);
        });
    }

    void FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOColor(new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1), 1f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(1f, RestartGame);
        });
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
