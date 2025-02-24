using UnityEngine;

public class TooltipSpotSign : MonoBehaviour
{
    ISpotSign spotSign;

    void Awake()
    {
        spotSign = GetComponent<ISpotSign>();
    }

    void OnMouseEnter()
    {
        TooltipManager.Instance.ShowTooltip(TooltipIconType.Coin, spotSign.price.ToString());
    }

    void OnMouseExit()
    {
        TooltipManager.Instance.HideTooltip();
    }
}
