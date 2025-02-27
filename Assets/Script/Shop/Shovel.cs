using DG.Tweening;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    const int grabOrderDiff = 3;

    [SerializeField] TimePresenter timePresenter;
    bool isGrabbing = false;
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
                IPlantSpot plantSpot = null;
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

                if (plantSpot != null && plantSpot.MyPlant != null)
                {
                    plantSpot.DigOut();
                }
                spriteRenderer.sortingOrder -= grabOrderDiff;
                ReturnInitPos();
            }
        }
    }

    void ReturnInitPos()
    {
        if (transform != null)
            transform.DOLocalMove(initPos, 0.3f).SetEase(Ease.OutCirc);
        else
            Debug.LogWarning("Transform NULL!");
    }

    void NightChanged()
    {
        isGrabbing = false;
        ReturnInitPos();
    }

    /*void OnDestroy()
    {
        TimePresenter.OnNightChanged -= NightChanged;
    }*/
}
