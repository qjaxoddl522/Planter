using UnityEngine;
using System.Collections.Generic;

public class Mushroom : PlantBase
{
    [Header("Mushroom")]
    [SerializeField] GameObject bulletPrefab;

    protected override void Ability()
    {
        List<Transform> targets = FindAllEnemies();
        foreach(Transform target in targets)
        {
            if (target != null && Mathf.Abs(target.position.x - transform.position.x) <= AttackRange)
            {
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<MushroomBullet>().Initialize(Power, target);

                InitCooltime();
            }
        }
    }

    // 벌은 인식하지 못하도록 오버라이딩
    protected override List<Transform> FindAllEnemies()
    {
        EnemyBase[] enemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);
        List<Transform> resultEnemies = new List<Transform>();

        foreach (EnemyBase enemy in enemies)
        {
            if ((isDirectionLeft && transform.position.x < enemy.transform.position.x) ||
            (!isDirectionLeft && transform.position.x > enemy.transform.position.x))
                continue;

            if (!enemy.IsHidden && enemy.enemyData.enemyID != Enemy.Bee)
            {
                resultEnemies.Add(enemy.transform);
            }
        }

        return resultEnemies;
    }
}
