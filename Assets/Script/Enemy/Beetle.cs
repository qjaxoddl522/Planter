using UnityEngine;

public class Beetle : EnemyBase
{
    protected override void AttackDamage()
    {
        Debug.Log("Beetle Attack");
    }
}
