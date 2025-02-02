using UnityEngine;

public interface ICoinManager : IManager
{
    int Coin { get; }
    void SetCoin(int amount);
    void AddCoin(int amount);
}

public class CoinManager : MonoBehaviour, ICoinManager
{
    ICoinUI coinUI;

    private int coin;
    public int Coin
    {
        get { return coin; }
        private set
        {
            coin = value;
            coinUI.UpdateUI(coin.ToString());
        }
    }

    public void SetCoin(int amount)
    {
        Coin = amount;
    }

    public void AddCoin(int amount)
    {
        Coin += amount;
    }

    public void Init()
    {
        coinUI = SystemLoader.Instance.Get<ICoinUI>();
        Coin = 10;
    }
}
