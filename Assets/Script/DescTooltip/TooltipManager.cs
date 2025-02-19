using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TooltipIconType
{
    Coin,
    Water,
}

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; }

    [SerializeField] GameObject tooltip;
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI priceText;

    [SerializeField] Sprite[] iconSprites;

    void Awake()
    {
        Instance = this;
    }

    void LateUpdate()
    {
        if (isTooltipActive())
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            tooltip.transform.position = mousePos;
        }
    }

    public void ShowTooltip(TooltipIconType icon, string text)
    {
        iconImage.sprite = iconSprites[(int)icon];
        priceText.text = text;
        tooltip.SetActive(true);
    }

    public void HideTooltip()
    {
        if (tooltip != null)
        {
            tooltip.SetActive(false);
        }
    }

    public bool isTooltipActive()
    {
        return tooltip.activeSelf;
    }
}
