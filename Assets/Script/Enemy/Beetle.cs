using UnityEngine;

public class Beetle : EnemyBase
{
    protected override void AttackDamage()
    {
        Debug.Log("Beetle Attack");
    }

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
