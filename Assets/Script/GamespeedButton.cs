using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GamespeedButton : MonoBehaviour, IHidedUI
{
    [SerializeField] Sprite[] sprites;
    Image buttonImage;
    bool isFaster = false;
    Vector3 initPos;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        Time.timeScale = 1.0f;
    }

    public void InitPosision(float dist)
    {
        initPos = transform.position;
        transform.position += new Vector3(0, dist, 0);
    }

    public void SlideIn(float duration)
    {
        transform.DOMove(initPos, duration).SetEase(Ease.OutCubic);
    }

    public void ChangeGamespeed()
    {
        if (!GameProcessManager.Instance.isGameEnd)
        {
            Time.timeScale = Time.timeScale > 1.5f ? 1f : 2f;
            UpdateIcon();
        }
    }

    public void ChangeGamespeed(float gameSpeed)
    {
        Time.timeScale = gameSpeed;
        UpdateIcon();
    }

    void UpdateIcon()
    {
        isFaster = (Time.timeScale > 1.5f);
        buttonImage.sprite = sprites[isFaster ? 1 : 0];
    }
}
