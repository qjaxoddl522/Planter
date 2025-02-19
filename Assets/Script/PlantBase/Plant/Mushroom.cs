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
            }
        }
    }
}
