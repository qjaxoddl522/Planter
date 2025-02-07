using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Scriptable Objects/PlantData")]
public class PlantData : ScriptableObject
{
    public Seed seedID;

    public int unlockPrice;
    public int price;

    public int hp;
    public float growthTime;
    public float abilityPeriod;
    public float abilityPower;

    public Sprite seedSprite;
    public Sprite plantSprite;
    public GameObject plantPrefab;
}
