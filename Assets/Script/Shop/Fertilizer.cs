using DG.Tweening;
using UnityEngine;

public class Fertilizer : MonoBehaviour
{
    const int grabOrderDiff = 3;

    [SerializeField] CoinPresenter coinPresenter;
    [SerializeField] TimePresenter timePresenter;
    public OtherSystemData sysData;
    public bool isGrabbing = false;
    Vector2 initPos;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        TimePresenter.OnNightChanged += NightChanged;
        initPos = transform.localPosition;
    }

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
                    isGrabbing = true;
                    spriteRenderer.sortingOrder += grabOrderDiff;
                }
                else
                    timePresenter.ShakeIcon();
            }
        }

        if (isGrabbing)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
            if (Input.GetMouseButtonUp(0))
            {
                isGrabbing = false;

                RaycastHit2D[] hitAll = Physics2D.RaycastAll(mousePos, Vector2.zero);
                PlantBase plantBase = null;
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

                if (plantBase != null && coinPresenter.TryBuy(sysData.fertilizerPrice))
                {
                    plantBase.Heal(plantBase.MaxHp);
                }
                spriteRenderer.sortingOrder -= grabOrderDiff;
                ReturnInitPos();
            }
        }
    }

    void ReturnInitPos()
    {
        transform.DOLocalMove(initPos, 0.3f).SetEase(Ease.OutCirc);
    }

    void NightChanged()
    {
        isGrabbing = false;
        ReturnInitPos();
    }
}
