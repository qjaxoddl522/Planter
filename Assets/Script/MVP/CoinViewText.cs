using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinViewText : MonoBehaviour
{
    TextMeshProUGUI coinText;
    Tween glitterTween;

    void Awake()
    {
        coinText = GetComponent<TextMeshProUGUI>();
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
