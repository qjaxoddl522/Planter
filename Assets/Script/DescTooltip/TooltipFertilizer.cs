using System;
using UnityEngine;

public class TooltipFertilizer : MonoBehaviour
{
    Fertilizer fertilizer;

    void Awake()
    {
        fertilizer = GetComponent<Fertilizer>();
    }

    void Start()
    {
        TimePresenter.OnNightChanged += NightChanged;
    }

    void Update()
    {
        if (fertilizer.isGrabbing)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlantBase plantBase = null;
            RaycastHit2D[] hitAll = Physics2D.RaycastAll(mousePos, Vector2.zero);
            foreach (RaycastHit2D hit in hitAll)
            {
                if (hit.collider != null)
                {
                    PlantBase plant = hit.collider.GetComponent<PlantBase>();
                    if (plant != null)
                    {
                        plantBase = plant;
                        break;
                    }
                }
            }

            if (plantBase != null)
            {
                TooltipManager.Instance.ShowTooltip(TooltipIconType.Coin, fertilizer.sysData.fertilizerPrice.ToString());
            }
            else
            {
                TooltipManager.Instance.HideTooltip();
            }

            if (Input.GetMouseButtonUp(0))
            {
                TooltipManager.Instance.HideTooltip();
            }
        }
    }

    void NightChanged()
    {
        if (fertilizer.isGrabbing)
            TooltipManager.Instance.HideTooltip();
    }

    void OnDestroy()
    {
        TimePresenter.OnNightChanged -= NightChanged;
    }
}
