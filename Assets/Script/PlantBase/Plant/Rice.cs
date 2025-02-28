using UnityEngine;

public class Rice : PlantBase
{
    [Header("Rice")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Sprite shootSprite;

    Animator animator;
    bool isReloaded = false;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (Cooltime > 0)
        {
            if (Cooltime <= MaxCooltime / 2 && !isReloaded)
            {
                animator.SetTrigger("Reload");
                isReloaded = true;
            }
            Cooltime -= Time.deltaTime;
        }
        else
            Ability();
    }

    protected override void Ability()
    {
        Transform target = FindStrongestEnemy();
        if (target != null)
        {
            Vector2 bulletPos = new Vector2(transform.position.x, transform.position.y + 0.6f);
            var bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
            bullet.GetComponent<RiceBullet>().Initialize(Power, target);
            
            animator.SetTrigger("Shoot");
            AudioManager.Instance.PlaySFX(AudioManager.SFX.ShootPlant);

            InitCooltime();
            isReloaded = false;
        }
    }

    Transform FindStrongestEnemy()
    {
        EnemyBase[] enemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);
        Transform weakestEnemy = null;
        int strongestHp = 0;

        foreach (EnemyBase enemy in enemies)
        {
            /*if ((isDirectionLeft && transform.position.x < enemy.transform.position.x) ||
            (!isDirectionLeft && transform.position.x > enemy.transform.position.x))
                continue;*/

            if (enemy.MaxHp >= strongestHp && !enemy.IsHidden)
            {
                strongestHp = enemy.MaxHp;
                weakestEnemy = enemy.transform;
            }
        }

        return weakestEnemy;
    }
}
