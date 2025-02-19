using UnityEngine;

public class Description : MonoBehaviour
{
    public DescriptionData data;

    bool isMouseOver = false;
    Collider2D myCollider;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (data != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool isInside = myCollider.bounds.Contains(mousePos);

            // 상태 변화 감지
            if (isInside)
            {
                isMouseOver = true;
                DescriptionManager.Instance.ShowDescription(data);
            }
            else if (isMouseOver && !Input.GetMouseButton(0))
            {
                isMouseOver = false;
                DescriptionManager.Instance.HideDescription();
            }
        }
    }

    void OnDestroy()
    {
        DescriptionManager.Instance.HideDescription();
    }
}