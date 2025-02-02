using UnityEngine;

public class PlantSpotGenerator : MonoBehaviour
{
    [SerializeField] GameObject plantSpotPrefab;
    [SerializeField] int spotWidth;

    void Start()
    {
        for (int i = 1; i <= spotWidth; i++)
        {
            var 
            inst = Instantiate(plantSpotPrefab, transform);
            inst.transform.position = new Vector2((0.5f + i), 0.5f);
            inst = Instantiate(plantSpotPrefab, transform);
            inst.transform.position = new Vector2(-(0.5f + i), 0.5f);
            inst = Instantiate(plantSpotPrefab, transform);
            inst.transform.position = new Vector2((0.5f + i), -0.5f);
            inst = Instantiate(plantSpotPrefab, transform);
            inst.transform.position = new Vector2(-(0.5f + i), -0.5f);
        }
    }
}
