using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimeViewDark : MonoBehaviour
{
    Image image;
    float originalAlpha;

    void Awake()
    {
        image = GetComponent<Image>();
        originalAlpha = image.color.a;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }

    public void UpdateDarkness(bool isDaytime)
    {
        image.DOFade(isDaytime ? 0 : originalAlpha, 1);
    }
}
