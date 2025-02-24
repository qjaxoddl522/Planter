using UnityEngine;

public class WaveInfo : MonoBehaviour
{
    [SerializeField] SpriteRenderer enemySprite;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite, float scale, bool isRight)
    {
        enemySprite.sprite = sprite;
        transform.localScale = new Vector2(scale, scale);
        spriteRenderer.flipX = isRight;
        enemySprite.flipX = isRight;
    }
}
