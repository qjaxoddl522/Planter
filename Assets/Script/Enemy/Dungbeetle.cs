using UnityEngine;

public class Dungbeetle : EnemyBase
{
    [SerializeField] ResistanceEffect resistanceEffect;

    protected override void Start()
    {
        base.Start();
        resistanceEffect.SetDirection(isDirectionLeft);
    }

    public override void TakeDamage(int damage, Seed attacker)
    {
        if (attacker == Seed.DefRanged)
        {
            resistanceEffect.PlayResistance();
            Hp -= damage / 2;
        }
        else
        {
            Hp -= damage;
        }

        if (Hp <= 0)
        {
            DestroyEnemy();
        }
        flashEffect.PlayWhiteFlash();
    }
}
