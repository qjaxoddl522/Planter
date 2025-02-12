using DG.Tweening;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    bool isGrabbing = false;
    Vector2 initPos;

    void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isGrabbing = true;
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
                transform.DOMove(initPos, 0.3f).SetEase(Ease.OutCirc);
            }
        }
    }
}
