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
    
    public void Init()
    {
        shopSeedObj = new GameObject[plantData.Length];
        CreateShop();
    }

    public void CreateShop()
    {
        foreach (var data in plantData)
        {
            CreateSeed(data.seedID);
        }
    }

    void CreateSeed(Seed seed)
    {
        int i = (int)seed;
        var seedInstance = Instantiate(buySeedPrefab, shopTransform);
        shopSeedObj[i] = seedInstance;
        seedInstance.name = plantData[i].ToString();
        seedInstance.GetComponent<ShopSeed>()._ShopManager = this;
        seedInstance.GetComponent<ShopSeed>().plantData = plantData[i];
        seedInstance.transform.position = new Vector3(
            -plantData.Length / 2 + i + (plantData.Length % 2 == 0 ? 0.5f : 0),
            seedInstance.transform.position.y);
        seedInstance.transform.DOScale(1, 0.8f).SetEase(Ease.OutElastic);
    }

    public void RefreshSeed(Seed seed)
    {
        Destroy(shopSeedObj[(int)seed]);
        CreateSeed(seed);
    }
    
    // 프리팹을 위한 메서드 옮기기
    public bool TryBuy(int amount)
    {
        return coinPresenter.TryBuy(amount);
    }
}
