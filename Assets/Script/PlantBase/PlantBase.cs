using UnityEngine;

public abstract class PlantBase : MonoBehaviour, IPlantable
{
    public PlantData plantData { get; set; }
    public CoinPresenter coinPresenter { get; set; }

    public bool isDirectionLeft;
    protected SpriteRenderer spriteRenderer;

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

    protected abstract void Ability();
}
