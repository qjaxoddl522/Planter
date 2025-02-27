using UnityEngine;

public class Corn : PlantBase
{
    [Header("Corn")]
    [SerializeField] GameObject bulletPrefab;

    protected override void Ability()
    {
        Transform target = FindClosestEnemy();
        if (target != null && Mathf.Abs(target.position.x - transform.position.x) <= AttackRange)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.ShootPlant);
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<CornBullet>().Initialize(Power, target);

            InitCooltime();
        }
    }
}
