using UnityEngine;

public class Greenbean : PlantBase
{
    [Header("Greenbean")]
    [SerializeField] GameObject bulletPrefab;

    protected override void Ability()
    {
        Transform target = FindClosestEnemy();
        if (target != null)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<GreenbeanBullet>().Initialize(Power, target);
        }
    }
}
