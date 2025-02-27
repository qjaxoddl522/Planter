using UnityEngine;

public class WaveInfo : MonoBehaviour
{
    [SerializeField] SpriteRenderer enemySprite;
    SpriteRenderer spriteRenderer;
    Description description;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        description = GetComponent<Description>();
    }

    public void Init(EnemyData enemyData, float scale, bool isRight)
    {
        enemySprite.sprite = enemyData.idleSprite;
        transform.localScale = new Vector2(scale, scale);
        spriteRenderer.flipX = isRight;
        enemySprite.flipX = isRight;

        description.data = enemyData.description;
    }
}
