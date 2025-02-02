using TMPro;
using UnityEngine;

public interface ICoinUI : IManager
{
    void UpdateUI(string amount);
}

public class CoinUI : MonoBehaviour, ICoinUI
{
    SystemLoader loader;
    [SerializeField] TextMeshProUGUI coinText;

    void OnEnable()
    {
        loader = SystemLoader.Instance;
        loader.Register<ICoinUI>(this);
    }

    public void Init()
    {
        
    }

    public void UpdateUI(string amount)
    {
        coinText.text = amount;
    }
}
