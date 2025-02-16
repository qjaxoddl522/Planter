using UnityEngine;

public class ResistanceEffect : MonoBehaviour
{
    public float duration = 0.5f;

    float durationTimer = 0f;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (durationTimer > 0)
        {
            durationTimer -= Time.deltaTime;
            SetAlpha(durationTimer / duration);
        }
        else
        {
            durationTimer = 0f;
            SetAlpha(0);
        }
    }

    public void SetDirection(bool isDirectionLeft)
    {
        if (isDirectionLeft)
        {
            transform.localPosition = new Vector3(-0.3f, transform.localPosition.y);
        }
        else
        {
            transform.localPosition = new Vector3(0.3f, transform.localPosition.y);
        }
    }

    public void PlayResistance()
    {
        durationTimer = duration;
        SetAlpha(1f);
    }

    void SetAlpha(float alpha)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
    }
}
