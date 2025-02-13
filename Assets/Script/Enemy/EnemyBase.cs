using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public EnemyData enemyData { get; set; }

    [Header("Status")]
    [SerializeField] int maxHp;
    protected int MaxHp
    {
        get { return maxHp; }
        private set { maxHp = value; }
    }

    [SerializeField] private int hp;
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

    [SerializeField] int range;
    public int Range
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
        private set { attackCooltime = value; }
    }

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    [Header("Data")]
    [SerializeField] GameObject effectPrefab;

    protected EnemyState currentState;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        MaxHp = enemyData.hp;
        Hp = MaxHp;
        Speed = enemyData.speed;
        Damage = enemyData.damage;
        Range = enemyData.range;
        AttackMaxCooltime = enemyData.attackPeriod;
        AttackCooltime = AttackMaxCooltime;

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
                    {
                        ChangeState(EnemyState.Attacking);
                    }
                    else
                    {
                        ChangeState(EnemyState.Walking);
                    }
                }
                break;
            case EnemyState.Walking:
                if (CheckForAttack())
                    ChangeState(EnemyState.Idle);
                else
                    transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
                break;
            case EnemyState.Attacking:
                break;
        }
    }

    bool CheckForAttack()
    {
        PlantBase closestPlant = FindClosestPlant();
        if (closestPlant != null)
        {
            float distanceToPlant = Mathf.Abs(transform.position.x - closestPlant.transform.position.x);
            return (distanceToPlant <= Range);
        }
        return false;
    }

    PlantBase FindClosestPlant()
    {
        PlantBase[] targets = FindObjectsByType<PlantBase>(FindObjectsSortMode.None);
        PlantBase closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (PlantBase target in targets)
        {
            float distance = Mathf.Abs(transform.position.x - target.transform.position.x);
            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    public void ChangeState(EnemyState newState)
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

    protected abstract void AttackDamage();
    public void EndAttack()
    {
        ChangeState(EnemyState.Idle);
    }

    public void DestroyEnemy()
    {
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
