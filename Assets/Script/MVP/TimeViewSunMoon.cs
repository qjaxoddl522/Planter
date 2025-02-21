using UnityEngine;
using UnityEngine.UI;

public class TimeViewSunMoon : MonoBehaviour
{
    [SerializeField] Sprite sun;
    [SerializeField] Sprite moon;
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void UpdateIcon(bool isDaytime)
    {
        image.sprite = isDaytime ? sun : moon;
    }
}
