using System;
using TMPro;
using UnityEngine;

public class StaticsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] enemyText;
    [SerializeField] TextMeshProUGUI plantCountText;
    [SerializeField] TextMeshProUGUI coinCountText;

    public int[] enemyCount;
    public static int plantCount;
    public int coinCount;

    void Awake()
    {
        enemyCount = new int[(int)Enemy.Count];
        plantCount = 0;
        coinCount = 0;
    }

    public void UpdateText()
    {
        for (int i = 0; i < enemyCount.Length; i++)
        {
            enemyText[i].text = enemyCount[i].ToString();
        }
        plantCountText.text = "½ÉÀº ½Ä¹°: " + plantCount;
        coinCountText.text = "È¹µæÇÑ ÄÚÀÎ: " + coinCount;
    }
}
