using DG.Tweening;
using UnityEngine;

public class MushroomBullet : MonoBehaviour
{
    int damage;
    Transform target;

    bool isDamaged = false;

    public void Initialize(int damage, Transform target)
    {
        this.damage = damage;
        this.target = target;
    }

    void Update()
    {
        if (target != null)
        {
            if (!isDamaged)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 3);
                if (Vector3.Distance(transform.position, target.position) < 0.1f)
                {
                    IEnemy attackable = target.GetComponent<IEnemy>();
                    if (attackable != null)
                    {
                        attackable.TakeDamage(damage, Seed.DefMelee);
                        attackable.SpeedAffect(-50f, StatModifierType.Percent, Seed.DefMelee, 2f);
                    }
                    isDamaged = true;
                    transform.GetComponent<SpriteRenderer>().DOFade(0, 2f).OnComplete(() => Destroy(gameObject));
                }
            }
            else
            {
                transform.position = target.position;
            }
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
