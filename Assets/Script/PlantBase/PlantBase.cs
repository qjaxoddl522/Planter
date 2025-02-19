using System;
using System.Collections.Generic;
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
    [Header("Interface")]
    public PlantData plantData { get; set; }
    public PlantSpot plantSpot { get; set; }
    public CoinPresenter coinPresenter { get; set; }
    public bool isDirectionLeft { get; set; }

    protected SpriteRenderer spriteRenderer;
    protected FlashEffect flashEffect;

    [Header("Status")]
    [SerializeField] Stat maxHp;
    protected int MaxHp { get { return maxHp.FinalValueInt; } }

    [SerializeField] int hp;
    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    [SerializeField] Stat maxCooltime;
    protected float MaxCooltime { get { return maxCooltime.FinalValue; } }

    [SerializeField] float cooltime;
    public float Cooltime
    {
        get { return cooltime; }
        set { cooltime = value; }
    }

    [SerializeField] Stat attackRange;
    public float AttackRange { get { return attackRange.FinalValue; } }

    [SerializeField] Stat power;
    public int Power{ get { return power.FinalValueInt; } }

    [Header("Prefab")]
    [SerializeField] GameObject effectPrefab;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashEffect = GetComponent<FlashEffect>();
    }

    void Start()
    {
        maxHp = new Stat(plantData.hp);
        Hp = MaxHp;
        maxCooltime = new Stat(plantData.abilityPeriod);
        Cooltime = MaxCooltime;
        attackRange = new Stat(plantData.abilityRange);
        power = new Stat(plantData.abilityPower);

        spriteRenderer.flipX = isDirectionLeft;
        if (GetComponent<Description>() is Description d)
            d.data = plantData.description;
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
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
            plantSpot.DigOut();
        }
        flashEffect.PlayWhiteFlash();
    }

    protected Transform FindClosestEnemy()
    {
        EnemyBase[] enemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);
        Transform closestEnemy = null;
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
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    protected List<Transform> FindAllEnemies()
    {
        EnemyBase[] enemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);
        List<Transform> resultEnemies = new List<Transform>();

        foreach (EnemyBase enemy in enemies)
        {
            if ((isDirectionLeft && transform.position.x < enemy.transform.position.x) ||
            (!isDirectionLeft && transform.position.x > enemy.transform.position.x))
                continue;

            if (!enemy.transform.GetComponent<IEnemy>().IsHidden)
            {
                resultEnemies.Add(enemy.transform);
            }
        }

        return resultEnemies;
    }

    protected abstract void Ability();
}
