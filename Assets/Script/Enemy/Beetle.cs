using UnityEngine;

public class Beetle : EnemyBase
{
    public override void TakeDamage(int damage, Seed attacker)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            DestroyEnemy();
        }
        flashEffect.PlayWhiteFlash();
    }
}
