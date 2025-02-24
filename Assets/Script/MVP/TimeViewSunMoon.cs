using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimeViewSunMoon : MonoBehaviour
{
    [SerializeField] Sprite sun;
    [SerializeField] Sprite moon;

    [Tooltip("1ȸ �¿��� ��� ���µ� �ɸ��� �ð�")]
    [SerializeField] float shakeSpeed;

    Image image;
    bool isShaking = false;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void UpdateIcon(bool isDaytime)
    {
        image.sprite = isDaytime ? sun : moon;
    }

    public void ShakeIcon()
    {
        if (!isShaking)
        {
            isShaking = true;
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOMoveX(transform.position.x + 0.1f, shakeSpeed / 2))
               .Append(transform.DOMoveX(transform.position.x - 0.1f, shakeSpeed))
               .Append(transform.DOMoveX(transform.position.x, shakeSpeed / 2))
               .SetLoops(2, LoopType.Restart)
               .OnComplete(() => isShaking = false);
        }
    }
}
