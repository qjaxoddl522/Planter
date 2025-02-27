using System;
using System.Collections;
using UnityEngine;

public interface IEnemy
{
    EnemyData enemyData { get; set; }
    void TakeDamage(int damage, Seed attacker);
    void Heal(int damage);
    void SpeedAffect(float value, StatModifierType type, float duration);
    bool IsHidden { get; set; }
}

public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    public EnemyData enemyData { get; set; }
    public event Action OnDeath;

    [Header("Status")]
    [SerializeField] Stat maxHp;
    public int MaxHp { get { return maxHp.FinalValueInt; } }

    [SerializeField] int hp;
    public int Hp
    {
        get { return hp; }
        set { hp = Math.Min(value, MaxHp); }
    }

    [SerializeField] Stat speed;
    public float Speed { get { return speed.FinalValue; } }

    [SerializeField] Stat damage;
    public int Damage { get { return damage.FinalValueInt; } }

    [SerializeField] Stat range;
    public float Range { get { return range.FinalValue; } }

    [SerializeField] Stat attackMaxCooltime;
    public float AttackMaxCooltime { get { return attackMaxCooltime.FinalValue; } }

    [SerializeField] float attackCooltime;
    public float AttackCooltime
    {
        get { return attackCooltime; }
        protected set { attackCooltime = value; }
    }

    [SerializeField] bool isHidden;
    public bool IsHidden
    {
        get { return isHidden; }
        set { isHidden = value; }
    }

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected bool isDirectionLeft;
    protected Transform closestPlant;

    [Header("Prefab")]
    [SerializeField] GameObject effectPrefab;
    [SerializeField] GameObject healParticlePrefab;

    protected EnemyState currentState;
    protected FlashEffect flashEffect;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        flashEffect = GetComponent<FlashEffect>();
    }

    protected virtual void Start()
    {
        maxHp = new Stat(enemyData.hp);
        Hp = MaxHp;
        speed = new Stat(enemyData.speed);
        damage = new Stat(enemyData.damage);
        range = new Stat(enemyData.range);
        attackMaxCooltime = new Stat(enemyData.attackPeriod);
        AttackCooltime = AttackMaxCooltime;
        isHidden = false;

        isDirectionLeft = transform.position.x > 0;
        spriteRenderer.flipX = isDirectionLeft;
        ChangeState(EnemyState.Walking);
    }

    protected virtual void Update()
    {
        AttackCooltime -= Time.deltaTime;
        spriteRenderer.sortingOrder = Modify.GetDepth(transform.position.y);

        // 현재 상태에서 계속 실행
        switch (currentState)
        {
            case EnemyState.Idle:
                if (AttackCooltime <= 0)
                {
                    if (CheckForAttack())
                        ChangeState(EnemyState.Attacking);
                    else
                        ChangeState(EnemyState.Walking);
                }
                break;
            case EnemyState.Walking:
                if (CheckForAttack())
                    ChangeState(EnemyState.Idle);
                else
                    transform.position += new Vector3(Speed * Time.deltaTime, 0, 0) * (isDirectionLeft ? -1 : 1);
                break;
            case EnemyState.Attacking:
                break;
        }
    }

    protected bool CheckForAttack()
    {
        closestPlant = FindClosestTarget();
        if (closestPlant != null)
        {
            float distanceToPlant = Mathf.Abs(transform.position.x - closestPlant.transform.position.x);
            return (distanceToPlant <= Range);
        }
        return false;
    }

    protected Transform FindClosestTarget()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,
            new Vector2(isDirectionLeft ? -1 : 1, 0),
            Mathf.Infinity,
            1 << (int)Layer.Plant);
        if (hitInfo.collider != null)
        {
            return hitInfo.collider.gameObject.transform;
        }
        return null;
    }

    public virtual void ChangeState(EnemyState newState)
    {
        // 종료 시 1회 실행
        switch (currentState)
        {
            case EnemyState.Idle:
                animator.ResetTrigger("Idle");
                break;
            case EnemyState.Walking:
                animator.ResetTrigger("Walk");
                break;
            case EnemyState.Attacking:
                animator.ResetTrigger("Attack");
                break;
        }

        // 시작 시 1회 실행
        switch (newState)
        {
            case EnemyState.Idle:
                animator.SetTrigger("Idle");
                spriteRenderer.sprite = enemyData.idleSprite;
                break;
            case EnemyState.Walking:
                animator.SetTrigger("Walk");
                break;
            case EnemyState.Attacking:
                animator.SetTrigger("Attack");
                AttackCooltime = AttackMaxCooltime;
                break;
        }

        currentState = newState;
    }

    protected virtual void AttackDamage()
    {
        if (closestPlant != null)
        {
            IHitable target = closestPlant.GetComponent<IHitable>();
            target?.TakeDamage(Damage);
        }
    }

    public void EndAttack()
    {
        ChangeState(EnemyState.Idle);
    }

    public abstract void TakeDamage(int damage, Seed attacker);
    public virtual void TakeDamagePostProcessing(Seed attacker)
    {
        if (Hp <= 0)
        {
            DestroyEnemy();
        }
        flashEffect.PlayWhiteFlash();
        AudioManager.Instance.PlaySFX(AudioManager.SFX.HitEnemy);
    }

    public void Heal(int damage)
    {
        Hp += damage;
        Instantiate(healParticlePrefab, transform.position, Quaternion.identity, transform);
    }

    public void SpeedAffect(float value, StatModifierType type, float duration)
    {
        StatModifier speedDebuff = new StatModifier(value, type, duration);
        speed.AddModifier(speedDebuff);
        StartCoroutine(RemoveModifierAfterDuration(speed, speedDebuff, duration));
    }

    IEnumerator RemoveModifierAfterDuration(Stat stat, StatModifier modifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        stat.RemoveModifier(modifier);
    }

    protected void DestroyEnemy()
    {
        OnDeath?.Invoke();
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
