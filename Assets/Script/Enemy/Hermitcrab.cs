using UnityEngine;

public class Hermitcrab : EnemyBase
{
    [SerializeField] ResistanceEffect resistanceEffect;

    protected override void Start()
    {
        base.Start();
        resistanceEffect.SetDirection(!isDirectionLeft);
    }

    public override void TakeDamage(int damage, Seed attacker)
    {
        if (attacker == Seed.DefHowitzer)
        {
            resistanceEffect.PlayResistance();
            Hp -= damage / 2;
        }
        else
        {
            Hp -= damage;
        }

        TakeDamagePostProcessing(attacker);
    }
}
