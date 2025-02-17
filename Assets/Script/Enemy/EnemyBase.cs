using UnityEngine;

public interface IEnemy
{
    void TakeDamage(int damage, Seed attacker);
    bool IsHidden { get; set; }
}

public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    public EnemyData enemyData { get; set; }

    [Header("Status")]
    [SerializeField] int maxHp;
    protected int MaxHp
    {
        get { return maxHp; }
        private set { maxHp = value; }
    }

    [SerializeField] int hp;
    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    [SerializeField] float speed;
    public float Speed
    {
        get { return speed; }
        private set { speed = value; }
    }

    [SerializeField] int damage;
    public int Damage
    {
        get { return damage; }
        private set { damage = value; }
    }

    [SerializeField] float range;
    public float Range
    {
        get { return range; }
        private set { range = value; }
    }

    [SerializeField] float attackMaxCooltime;
    public float AttackMaxCooltime
    {
        get { return attackMaxCooltime; }
        private set { attackMaxCooltime = value; }
    }

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
    protected PlantBase closestPlant;

    [Header("Prefab")]
    [SerializeField] GameObject effectPrefab;

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
        MaxHp = enemyData.hp;
        Hp = MaxHp;
        Speed = enemyData.speed;
        Damage = enemyData.damage;
        Range = enemyData.range;
        AttackMaxCooltime = enemyData.attackPeriod;
        AttackCooltime = AttackMaxCooltime;
        isHidden = false;

        isDirectionLeft = transform.position.x > 0;
        spriteRenderer.flipX = isDirectionLeft;
        ChangeState(EnemyState.Walking);
    }

    void Update()
    {
        AttackCooltime -= Time.deltaTime;

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
        closestPlant = FindClosestPlant();
        if (closestPlant != null)
        {
            float distanceToPlant = Mathf.Abs(transform.position.x - closestPlant.transform.position.x);
            return (distanceToPlant <= Range);
        }
        return false;
    }

    protected PlantBase FindClosestPlant()
    {
        PlantBase[] targets = FindObjectsByType<PlantBase>(FindObjectsSortMode.None);
        PlantBase closestTarget = null;
        float closestDistance = Mathf.Infinity;
        float x = transform.position.x;

        foreach (PlantBase target in targets)
        {
            float targetX = target.transform.position.x;

            if ((isDirectionLeft && (x < targetX || targetX < 0)) ||
            (!isDirectionLeft && (x > targetX || targetX > 0)))
                continue;

            float distance = Mathf.Abs(x - targetX);
            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
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
                spriteRenderer.sprite = enemyData.IdleSprite;
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
        closestPlant?.TakeDamage(Damage);
    }

    public void EndAttack()
    {
        ChangeState(EnemyState.Idle);
    }

    public abstract void TakeDamage(int damage, Seed attacker);
    protected void DestroyEnemy()
    {
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
