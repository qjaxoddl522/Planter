using UnityEngine;

public interface IPlantable
{
    PlantData plantData { get; set; }
    PlantSpot plantSpot { get; set; }
    CoinPresenter coinPresenter { get; set; }
    bool isDirectionLeft { get; set; }
    void DestroyPlant();
}

public abstract class PlantBase : MonoBehaviour, IPlantable
{
    public PlantData plantData { get; set; }
    public PlantSpot plantSpot { get; set; }
    public CoinPresenter coinPresenter { get; set; }
    public bool isDirectionLeft { get; set; }

    protected SpriteRenderer spriteRenderer;

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

    [SerializeField] float maxCooltime;
    protected float MaxCooltime
    {
        get { return maxCooltime; }
        private set { maxCooltime = value; }
    }

    [SerializeField] float cooltime;
    public float Cooltime
    {
        get { return cooltime; }
        set { cooltime = value; }
    }

    [SerializeField] int power;
    public int Power
    {
        get { return power; }
        private set { power = value; }
    }


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        spriteRenderer.flipX = isDirectionLeft;
        MaxHp = plantData.hp;
        Hp = MaxHp;
        MaxCooltime = plantData.abilityPeriod;
        Cooltime = MaxCooltime;
        power = plantData.abilityPower;
    }

    void Update()
    {
        if (Cooltime > 0)
        {
            Cooltime -= Time.deltaTime;
        }
        else
        {
            Ability();
            Cooltime = MaxCooltime;
        }
    }

    public void DestroyPlant()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            DestroyPlant();
        }
    }

    public Transform FindClosestEnemy()
    {
        EnemyBase[] enemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);
        EnemyBase closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (EnemyBase enemy in enemies)
        {
            if ((isDirectionLeft && transform.position.x < enemy.transform.position.x) ||
            (!isDirectionLeft && transform.position.x > enemy.transform.position.x))
                continue;
            
            float distance = Mathf.Abs(transform.position.x - enemy.transform.position.x);
            if (distance <= closestDistance && !enemy.transform.GetComponent<IEnemy>().IsHidden)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy != null ? closestEnemy.transform : null;
    }

    protected abstract void Ability();
}
