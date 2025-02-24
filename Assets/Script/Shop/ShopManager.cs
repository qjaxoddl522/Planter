using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public interface IShopManager
{
    void CreateShop();
    void RefreshSeed(Seed seed);
    bool TryBuy(int amount);
}

public class ShopManager : MonoBehaviour, IShopManager
{
    [SerializeField] CoinPresenter coinPresenter;
    [SerializeField] TimePresenter timePresenter;
    [SerializeField] Transform shopTransform;
    [SerializeField] GameObject buySeedPrefab;
    [SerializeField] PlantData[] plantData;

    Dictionary<Seed, GameObject> shopSeedObj;
    Dictionary<Seed, bool> isSeedAvailable;

    void Start()
    {
        shopSeedObj = new Dictionary<Seed, GameObject>();
        isSeedAvailable = new Dictionary<Seed, bool>();
        CreateShop();
    }

    public void CreateShop()
    {
        foreach (var data in plantData)
        {
            isSeedAvailable[data.seedID] = data.unlockPrice <= 0;
            CreateSeed(data.seedID);
        }
    }

    void CreateSeed(Seed seed)
    {
        int i = (int)seed;
        var seedInstance = Instantiate(buySeedPrefab, shopTransform);
        shopSeedObj[seed] = seedInstance;
        seedInstance.name = plantData[i].ToString();
        seedInstance.transform.position = new Vector3(
            (-plantData.Length / 2 + i) * 1f + (plantData.Length % 2 == 0 ? 0.5f : 0),
            shopTransform.position.y);

        var shopSeed = seedInstance.GetComponent<IShopSeed>();
        shopSeed.shopManager = this;
        shopSeed.coinPresenter = coinPresenter;
        shopSeed.timePresenter = timePresenter;
        shopSeed.plantData = plantData[i];
        shopSeed.isAvailable = isSeedAvailable[seed];
        shopSeed.OnSeedUnlocked += HandleSeedUnlocked;
    }

    public void RefreshSeed(Seed seed)
    {
        Destroy(shopSeedObj[seed]);
        CreateSeed(seed);
    }

    void HandleSeedUnlocked(IShopSeed shopSeed)
    {
        isSeedAvailable[shopSeed.plantData.seedID] = true;
    }
    
    // 프리팹을 위한 메서드 옮기기
    public bool TryBuy(int amount)
    {
        return coinPresenter.TryBuy(amount);
    }
}
