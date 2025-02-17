using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public interface IShopManager : IManager
{
    void CreateShop();
    void RefreshSeed(Seed seed);
    bool TryBuy(int amount);
}

public class ShopManager : MonoBehaviour, IShopManager
{
    [SerializeField] CoinPresenter coinPresenter;
    [SerializeField] Transform shopTransform;
    [SerializeField] GameObject buySeedPrefab;
    [SerializeField] PlantData[] plantData;
    
    GameObject[] shopSeedObj;
    bool[] isSeedAvailable;

    public void Init()
    {
        shopSeedObj = new GameObject[plantData.Length];
        isSeedAvailable = new bool[plantData.Length];
        CreateShop();
    }

    public void CreateShop()
    {
        foreach (var data in plantData)
        {
            isSeedAvailable[(int)data.seedID] = data.unlockPrice <= 0;
            CreateSeed(data.seedID);
        }
    }

    void CreateSeed(Seed seed)
    {
        int i = (int)seed;
        var seedInstance = Instantiate(buySeedPrefab, shopTransform);
        shopSeedObj[i] = seedInstance;
        seedInstance.name = plantData[i].ToString();
        seedInstance.transform.position = new Vector3(
            -plantData.Length / 2 + i + (plantData.Length % 2 == 0 ? 0.5f : 0),
            seedInstance.transform.position.y);

        var shopSeed = seedInstance.GetComponent<IShopSeed>();
        shopSeed.shopManager = this;
        shopSeed.coinPresenter = coinPresenter;
        shopSeed.plantData = plantData[i];
        shopSeed.isAvailable = isSeedAvailable[i];
        shopSeed.OnSeedUnlocked += HandleSeedUnlocked;
    }

    public void RefreshSeed(Seed seed)
    {
        Destroy(shopSeedObj[(int)seed]);
        CreateSeed(seed);
    }

    void HandleSeedUnlocked(IShopSeed shopSeed)
    {
        isSeedAvailable[(int)shopSeed.plantData.seedID] = true;
    }
    
    // 프리팹을 위한 메서드 옮기기
    public bool TryBuy(int amount)
    {
        return coinPresenter.TryBuy(amount);
    }
}
