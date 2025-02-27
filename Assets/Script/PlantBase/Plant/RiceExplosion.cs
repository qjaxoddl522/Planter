using System.Collections.Generic;
using UnityEngine;

public class RiceExplosion : MonoBehaviour
{
    int damage;

    public void Initialize(int damage)
    {
        this.damage = damage;
    }

    void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
        contactFilter.useLayerMask = true;

        List<Collider2D> results = new List<Collider2D>();

        collider.Overlap(contactFilter, results);
        foreach (Collider2D col in results)
        {
            col.GetComponent<IEnemy>().TakeDamage(damage, Seed.BufAttack);
        }
        AudioManager.Instance.PlaySFX(AudioManager.SFX.Explosion);
    }

    public void AnimationEnd() { Destroy(gameObject); }
}
