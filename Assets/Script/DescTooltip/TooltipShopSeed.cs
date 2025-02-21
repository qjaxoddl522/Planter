using UnityEngine;

public class TooltipShopSeed : MonoBehaviour
{
    IShopSeed shopSeed;

    void Awake()
    {
        shopSeed = GetComponent<IShopSeed>();
    }

    void Start()
    {
        shopSeed.OnSeedUnlocked += SeedUnlock;
        shopSeed.OnSeedDropped += SeedDrop;
        TimePresenter.OnNightChanged += NightChanged;
    }

    void Update()
    {
        if (shopSeed.isGrabbing)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;

            IPlantSpot plantSpot = null;
            RaycastHit2D[] hitAll = Physics2D.RaycastAll(mousePos, Vector2.zero);
            foreach (RaycastHit2D hit in hitAll)
            {
                if (hit.collider != null)
                {
                    IPlantSpot spot = hit.collider.GetComponent<IPlantSpot>();
                    if (spot != null)
                    {
                        plantSpot = spot;
                        break;
                    }
                }
            }

            if (plantSpot != null && plantSpot.MyPlant == null)
            {
                TooltipManager.Instance.ShowTooltip(TooltipIconType.Coin, shopSeed.plantData.price.ToString());
            }
            else
            {
                TooltipManager.Instance.HideTooltip();
            }
        }
    }

    void OnMouseEnter()
    {
        if (!shopSeed.isAvailable)
            TooltipManager.Instance.ShowTooltip(TooltipIconType.Coin, shopSeed.plantData.unlockPrice.ToString());
    }

    void OnMouseExit()
    {
        if (!shopSeed.isGrabbing)
            TooltipManager.Instance.HideTooltip();
    }

    void SeedUnlock(IShopSeed seed) => TooltipManager.Instance.HideTooltip();
    void SeedDrop() => TooltipManager.Instance.HideTooltip();

    void NightChanged()
    {
        if (shopSeed.isGrabbing)
            TooltipManager.Instance.HideTooltip();
    }

    void OnDestroy()
    {
        TimePresenter.OnNightChanged -= NightChanged;
    }
}
