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
            var 
            inst = Instantiate(plantSpotPrefab, transform);
            inst.transform.position = new Vector2((0.5f + i * 1.2f), 0.5f);
            inst.GetComponent<IPlantSpot>().coinPresenter = coinPresenter;
            inst = Instantiate(plantSpotPrefab, transform);
            inst.transform.position = new Vector2(-(0.5f + i * 1.2f), 0.5f);
            inst.GetComponent<IPlantSpot>().coinPresenter = coinPresenter;
            inst = Instantiate(plantSpotPrefab, transform);
            inst.transform.position = new Vector2((0.5f + i * 1.2f), -0.5f);
            inst.GetComponent<IPlantSpot>().coinPresenter = coinPresenter;
            inst = Instantiate(plantSpotPrefab, transform);
            inst.transform.position = new Vector2(-(0.5f + i * 1.2f), -0.5f);
            inst.GetComponent<IPlantSpot>().coinPresenter = coinPresenter;
        }
    }
}
