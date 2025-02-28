using UnityEngine;

public class CoinPresenter : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] CoinModel mCoin;

    [Header("View")]
    [SerializeField] CoinViewText vCoinText;
    [SerializeField] StaticsManager staticsManager;

    void Start()
    {
        mCoin.CoinChanged += CoinChanged;
        mCoin.Init();
    }

    void OnDestroy()
    {
        mCoin.CoinChanged -= CoinChanged;
    }

    public bool TryBuy(int amount)
    {
        if (mCoin.Coin >= amount)
        {
            mCoin.Decrement(amount);
            return true;
        }
        vCoinText.GlitterText();
        return false;
    }

    public void GetCoin(int amount)
    {
        staticsManager.coinCount += amount;
        mCoin.Increment(amount);
    }

    public void CoinChanged()
    {
        UpdateView();
    }

    void UpdateView()
    {
        vCoinText.UpdateText(mCoin.Coin.ToString());
    }
}
