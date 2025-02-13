using UnityEngine;

public interface IPlantSpot
{
    IPlantable MyPlant { get; set; }
    CoinPresenter coinPresenter { get; set; }
    void Plant(PlantData plantData);
    void DigOut();
}

public class PlantSpot : MonoBehaviour, IPlantSpot
{
    [SerializeField] GameObject sproutPrefab;

    public IPlantable MyPlant { get; set; } = null;
    public CoinPresenter coinPresenter { get; set; }

    public void Plant(PlantData plantData)
    {
        if (MyPlant != null)
        {
            Debug.LogWarning("�̹� �Ĺ��� �ɾ��� �ֽ��ϴ�.");
            return;
        }

        var plantInstance = Instantiate(sproutPrefab, transform);
        MyPlant = plantInstance.GetComponent<IPlantable>();

        MonoBehaviour plantMono = MyPlant as MonoBehaviour;
        if (plantMono != null)
        {
            plantMono.transform.localPosition = Vector2.zero;
            MyPlant.plantData = plantData;
            MyPlant.plantSpot = this;
            MyPlant.coinPresenter = coinPresenter;
        }
    }

    public void DigOut()
    {
        if (MyPlant == null)
        {
            Debug.LogWarning("�Ĺ��� �����ϴ�.");
            return;
        }
        
        MyPlant.DestroyPlant();
        MyPlant = null;
    }
}
