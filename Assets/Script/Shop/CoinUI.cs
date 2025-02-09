using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    public void Init()
    {
        
    }

    public void UpdateUI(string amount)
    {
        coinText.text = amount;
    }
}
