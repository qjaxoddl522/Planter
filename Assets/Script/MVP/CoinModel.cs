using System;
using UnityEngine;

public class CoinModel : MonoBehaviour
{
    const int initCoin = 10;

    public event Action CoinChanged;

    private int coin;
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
        Coin = initCoin;
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
