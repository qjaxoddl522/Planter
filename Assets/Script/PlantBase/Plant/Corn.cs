using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Corn : PlantBase
{
    [Header("Corn")]
    [SerializeField] GameObject bulletPrefab;

    protected override void Ability()
    {
        Transform target = FindWeakestEnemy();
        if (target != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.ShootPlant);
            Vector3 shootPos = new Vector3(transform.position.x, transform.position.y + 0.5f);
            var bullet = Instantiate(bulletPrefab, shootPos, Quaternion.identity);
            bullet.GetComponent<CornBullet>().Initialize(Power, target);

            InitCooltime();
        }
    }

    Transform FindWeakestEnemy()
    {
        EnemyBase[] enemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);
        Transform weakestEnemy = null;
        float closestDistance = float.MaxValue;
        int weakestHp = int.MaxValue;

        foreach (EnemyBase enemy in enemies)
        {
            /*if ((isDirectionLeft && transform.position.x < enemy.transform.position.x) ||
            (!isDirectionLeft && transform.position.x > enemy.transform.position.x))
                continue;*/

            float distance = Mathf.Abs(transform.position.x - enemy.transform.position.x);
            if ((enemy.MaxHp < weakestHp ||
                (enemy.MaxHp == weakestHp && distance < closestDistance)) &&
                !enemy.IsHidden)
            {
                if (Mathf.Abs(enemy.transform.position.x - transform.position.x) <= AttackRange)
                {
                    closestDistance = distance;
                    weakestHp = enemy.MaxHp;
                    weakestEnemy = enemy.transform;
                }
            }
        }

        return weakestEnemy;
    }
}
