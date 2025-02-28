using UnityEngine;

public class Corn : PlantBase
{
    [Header("Corn")]
    [SerializeField] GameObject bulletPrefab;

    protected override void Ability()
    {
        Transform target = FindWeakestEnemy();
        if (target != null && Mathf.Abs(target.position.x - transform.position.x) <= AttackRange)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.ShootPlant);
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<CornBullet>().Initialize(Power, target);

            InitCooltime();
        }
    }

    Transform FindWeakestEnemy()
    {
        EnemyBase[] enemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);
        Transform weakestEnemy = null;
        int weakestHp = int.MaxValue;

        foreach (EnemyBase enemy in enemies)
        {
            if ((isDirectionLeft && transform.position.x < enemy.transform.position.x) ||
            (!isDirectionLeft && transform.position.x > enemy.transform.position.x))
                continue;

            if (enemy.MaxHp <= weakestHp && !enemy.IsHidden)
            {
                weakestHp = enemy.MaxHp;
                weakestEnemy = enemy.transform;
            }
        }

        return weakestEnemy;
    }
}
