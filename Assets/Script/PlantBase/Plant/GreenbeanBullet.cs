using UnityEngine;

public class GreenbeanBullet : MonoBehaviour
{
    int damage;
    Transform target;
    SpriteRenderer spriteRenderer;

    public void Initialize(int damage, Transform target)
    {
        this.damage = damage;
        this.target = target;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 5);
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                IEnemy attackable = target.GetComponent<IEnemy>();
                if (attackable != null)
                {
                    attackable.TakeDamage(damage, Seed.DefRanged);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }

        spriteRenderer.sortingOrder = Modify.GetDepth(transform.position.y);
    }
}
