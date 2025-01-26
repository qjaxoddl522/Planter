using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int shopSize { get; set; } = 3;
    public List<seed> seedList = new List<seed>();
    
    void Start()
    {
        
    }

    public void Init()
    {
        seedList.Add(seed.coin);
    }
}
