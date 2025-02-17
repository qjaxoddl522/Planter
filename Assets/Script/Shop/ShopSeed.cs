using DG.Tweening;
using System;
using UnityEngine;

public interface IShopSeed
{
    IShopManager shopManager { get; set; }
    CoinPresenter coinPresenter { get; set; }
    PlantData plantData { get; set; }
    bool isAvailable { get; set; }
    event Action<IShopSeed> OnSeedUnlocked;
}

public class ShopSeed : MonoBehaviour, IShopSeed
{
    [SerializeField] GameObject lockPrefab;
    GameObject lockInstance;

    public IShopManager shopManager { get; set; }
    public CoinPresenter coinPresenter { get; set; }
    public PlantData plantData { get; set; }
    public bool isAvailable { get; set; }
    public event Action<IShopSeed> OnSeedUnlocked;

    SpriteRenderer spriteRenderer;
    bool isGrabbing = false;
    Vector2 initPos;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        initPos = transform.position;
        spriteRenderer.sprite = plantData.seedSprite;
        
        if (isAvailable)
        {
            transform.localScale = Vector2.zero;
            transform.DOScale(1, 0.8f).SetEase(Ease.OutElastic);
        }
        else
        {
            lockInstance = Instantiate(lockPrefab, transform);
            lockInstance.transform.localPosition = Vector2.zero;
            spriteRenderer.color = new Color(0.6f, 0.6f, 0.6f, 1);
        }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (isAvailable)
                {
                    isGrabbing = true;
                }
                else if (coinPresenter.TryBuy(plantData.unlockPrice))
                {
                    UnlockSeed();
                }
            }
        }

        if (isGrabbing)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
            if (Input.GetMouseButtonUp(0))
            {
                isGrabbing = false;

                RaycastHit2D[] hitAll = Physics2D.RaycastAll(mousePos, Vector2.zero);
                IPlantSpot plantSpot = null;
                foreach (RaycastHit2D hit in hitAll)
                {
                    if (hit.collider != null)
                    {
                        IPlantSpot spot = hit.collider.GetComponent<IPlantSpot>();
                        if (spot != null)
                        {
                            plantSpot = spot;
                            break;
                        }
                    }
                }

                if (plantSpot != null && plantSpot.MyPlant == null && shopManager.TryBuy(plantData.price))
                {
                    plantSpot.Plant(plantData);
                    shopManager.RefreshSeed(plantData.seedID);
                }
                else
                {
                    transform.DOMove(initPos, 0.3f).SetEase(Ease.OutCirc);
                }
            }
        }
    }

    public void UnlockSeed()
    {
        spriteRenderer.color = Color.white;
        Destroy(lockInstance);
        isAvailable = true;
        OnSeedUnlocked?.Invoke(this);
    }
}
