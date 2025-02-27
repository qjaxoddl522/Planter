using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite mouseSprite;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            if (Input.GetMouseButtonDown(0) && !GameProcessManager.Instance.isGameStart)
            {
                AudioManager.Instance.PlaySFX(AudioManager.SFX.ShootPlant);
                GameProcessManager.Instance.GameStart();
            }
            spriteRenderer.sprite = mouseSprite;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
        }
    }
}
