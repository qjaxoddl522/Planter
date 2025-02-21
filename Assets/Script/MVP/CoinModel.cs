using System;
using UnityEngine;

public class CoinModel : MonoBehaviour
{
    [SerializeField] OtherSystemData sysData;

    public event Action CoinChanged;
    
    [SerializeField] private int coin;
    public int Coin
    {
        get { return coin; }
        private set
        {
            coin = value;
            CoinChanged?.Invoke();
        }
    }

    public void Init()
    {
        Coin = sysData.initCoin;
    }

    public void Set(int amount)
    {
        Coin = amount;
    }

    public void Increment(int amount)
    {
        Coin += amount;
    }

    public void Decrement(int amount)
    {
        Coin -= amount;
    }
}
