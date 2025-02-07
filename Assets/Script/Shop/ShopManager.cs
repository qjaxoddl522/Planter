using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using DG.Tweening;

public interface IShopManager : IManager
{
    void CreateShop();
    void RefreshShop();
    bool TryBuy(int amount);
}

public class ShopManager : MonoBehaviour, IShopManager
{
    public int shopLen { get; set; } = 5;
    public List<Seed> seedList = new List<Seed>();
    List<GameObject> seedObj = new List<GameObject>();

    [SerializeField] Transform shopTransform;
    [SerializeField] GameObject buySeedPrefab;
    [SerializeField] PlantData[] plantData;
    ICoinManager _coinManager;
    
    public void Init()
    {
        _coinManager = SystemLoader.Instance.Get<ICoinManager>();
        CreateShop();
    }

    public void CreateShop()
    {
        seedList.Clear();
        List<Seed> seedPool = plantData
            .Select(p => p.seedID)
            .ToList();
        for (int i = 0; i < shopLen; i++)
        {
            Seed seed = seedPool[Random.Range(0, seedPool.Count)];
            seedList.Add(seed);
            seedPool.Remove(seed);
        }

        for (int i=0; i<seedList.Count; i++)
        {
            var seedInstance = Instantiate(buySeedPrefab, shopTransform);
            seedObj.Add(seedInstance);
            seedInstance.name = seedList[i].ToString();
            seedInstance.GetComponent<ShopSeed>()._ShopManager = this;
            seedInstance.GetComponent<ShopSeed>().plantData = plantData[(int)seedList[i]];
            seedInstance.transform.position =new Vector3(
                -seedList.Count / 2 + i + (seedList.Count % 2 == 0 ? 0.5f : 0 ),
                seedInstance.transform.position.y);

            seedInstance.transform.DOScale(1, 0.8f).SetEase(Ease.OutElastic);
        }
    }

    public void RefreshShop()
    {
        foreach (var obj in seedObj)
        {
            Destroy(obj);
        }
        seedObj.Clear();
        CreateShop();
    }

    public bool TryBuy(int amount)
    {
        if (_coinManager.Coin >= amount)
        {
            _coinManager.AddCoin(-amount);
            return true;
        }
        return false;
    }
}
