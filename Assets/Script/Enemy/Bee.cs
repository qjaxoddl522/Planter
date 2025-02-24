using System.Collections.Generic;
using UnityEngine;

public class Bee : EnemyBase
{
    EnemyBeeData beeData;
    float plateCooltime;

    override protected void Start()
    {
        base.Start();

        beeData = enemyData as EnemyBeeData;
        InitPlateCooltime();
    }

    protected override void Update()
    {
        base.Update();

        plateCooltime -= Time.deltaTime;
        if (plateCooltime <= 0 )
        {
            foreach (IEnemy enemy in FindPlateEnemies())
            {
                if (enemy != null) enemy.Heal(beeData.platePower);
            }
            InitPlateCooltime();
        }
    }

    public override void TakeDamage(int damage, Seed attacker)
    {
        Hp -= damage;
        TakeDamagePostProcessing(attacker);
    }

    void InitPlateCooltime()
    {
        plateCooltime = beeData.platePeriod;
    }

    List<IEnemy> FindPlateEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, beeData.plateRange, 1 << (int)Layer.Enemy);

        List<IEnemy> enemies = new List<IEnemy>();
        foreach (var hit in hits)
        {
            IEnemy enemy = hit.GetComponent<IEnemy>();
            if (enemy != null && !enemy.IsHidden && enemy.enemyData.enemyID != Enemy.Bee)
            {
                enemies.Add(enemy);
            }
        }

        return enemies;
    }
}
