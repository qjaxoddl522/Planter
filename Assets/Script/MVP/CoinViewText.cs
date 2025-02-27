using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinViewText : MonoBehaviour, IHidedUI
{
    [SerializeField] TextMeshProUGUI coinText;
    Tween glitterTween;
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

    public void UpdateText(string str)
    {
        coinText.text = str;
    }

    public void GlitterText()
    {
        if (glitterTween == null || !glitterTween.IsPlaying())
            glitterTween = coinText.DOColor(Color.red, 0.1f).SetLoops(4, LoopType.Yoyo);
    }
}
