using System;
using UnityEngine;

public interface ISpotSign
{
    CoinPresenter coinPresenter { get; set; }
    TimePresenter timePresenter { get; set; }
    bool isLeft { get; set; }
    event Action<bool> OnSpotExtended;
    int price { get; set; }
}

public class SpotSign : MonoBehaviour, ISpotSign
{
    public CoinPresenter coinPresenter { get; set; }
    public TimePresenter timePresenter { get; set; }
    public bool isLeft { get; set; }
    public event Action<bool> OnSpotExtended;
    public int price { get; set; }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (TimePresenter.isDaytime)
                {
                    if (coinPresenter.TryBuy(price))
                    {
                        OnSpotExtended?.Invoke(isLeft);
                        AudioManager.Instance.PlaySFX(AudioManager.SFX.CashMachine);
                        TooltipManager.Instance.HideTooltip();
                    }
                }
                else
                {
                    timePresenter.ShakeIcon();
                }
            }
        }
    }
}
