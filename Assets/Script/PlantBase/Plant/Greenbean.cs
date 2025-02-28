using UnityEngine;

public class Greenbean : PlantBase
{
    [Header("Greenbean")]
    [SerializeField] GameObject bulletPrefab;

    protected override void Ability()
    {
        Transform target = FindClosestEnemy();
        if (target != null && Mathf.Abs(target.position.x - transform.position.x) <= AttackRange)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.ShootPlant);
            Vector3 shootPos = new Vector3(transform.position.x, transform.position.y + 0.5f);
            var bullet = Instantiate(bulletPrefab, shootPos, Quaternion.identity);
            bullet.GetComponent<GreenbeanBullet>().Initialize(Power, target);

            InitCooltime();
        }
    }

    // 벌은 인식하지 못하도록 오버라이딩
    protected override Transform FindClosestEnemy()
    {
        EnemyBase[] enemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (EnemyBase enemy in enemies)
        {
            if ((isDirectionLeft && transform.position.x < enemy.transform.position.x) ||
            (!isDirectionLeft && transform.position.x > enemy.transform.position.x))
                continue;

            float distance = Mathf.Abs(transform.position.x - enemy.transform.position.x);
            if (distance <= closestDistance && !enemy.IsHidden && enemy.enemyData.enemyID != Enemy.Bee)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }
}
