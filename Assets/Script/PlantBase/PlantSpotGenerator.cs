using UnityEngine;

public class PlantSpotGenerator : MonoBehaviour
{
    [SerializeField] CoinPresenter coinPresenter;
    [SerializeField] GameObject plantSpotPrefab;
    [SerializeField] int spotWidth;

    void Start()
    {
        for (int i = 1; i <= spotWidth; i++)
        {
            CreatePlantSpot(new Vector2((0.5f + i * 1.2f), 0.5f));
            CreatePlantSpot(new Vector2(-(0.5f + i * 1.2f), 0.5f));
            CreatePlantSpot(new Vector2((0.5f + i * 1.2f), -0.5f));
            CreatePlantSpot(new Vector2(-(0.5f + i * 1.2f), -0.5f));
        }
    }

    void CreatePlantSpot(Vector2 pos)
    {
        var inst = Instantiate(plantSpotPrefab, transform);
        inst.transform.position = pos;
        inst.GetComponent<IPlantSpot>().coinPresenter = coinPresenter;
    }
}
