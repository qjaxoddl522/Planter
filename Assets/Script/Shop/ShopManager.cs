using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public interface IShopManager: IManager
{
    void RefreshShop();
    void CreateShop();
}

public class ShopManager : MonoBehaviour, IShopManager
{
    public int shopLen { get; set; } = 3;
    public List<Seed> seedList = new List<Seed>();
    [SerializeField] Transform shopObject;
    [SerializeField] GameObject buySeed;
    [SerializeField] Sprite[] seedSprites;
    int seedLen;
    
    public void Init()
    {
        seedLen = seedSprites.Length;
        List<Seed> seedPool = Enum.GetValues(typeof(Seed)).Cast<Seed>().ToList();
        for (int i = 0; i < shopLen; i++)
        {
            Seed seed = seedPool[Random.Range(0, seedPool.Count)];
            seedList.Add(seed);
            seedPool.Remove(seed);
        }
    }

    public void CreateShop()
    {
        for (int i=0; i<seedList.Count; i++)
        {
            var seedInstance = Instantiate(buySeed, shopObject);
            seedInstance.name = seedList[i].ToString();
            seedInstance.GetComponent<SpriteRenderer>().sprite = seedSprites[(int)seedList[i]];
            seedInstance.transform.position =new Vector3(
                -seedList.Count / 2 + i + (seedList.Count % 2 == 0 ? 0.5f : 0 ),
                seedInstance.transform.position.y);
        }
    }

    public void RefreshShop()
    {

    }
}
