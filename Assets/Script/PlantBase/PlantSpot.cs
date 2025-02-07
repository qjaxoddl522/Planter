using UnityEngine;

public interface IPlantSpot
{
    IPlantable MyPlant { get; set; }
    void Plant(PlantData plantData);
}

public class PlantSpot : MonoBehaviour, IPlantSpot
{
    [SerializeField] GameObject sproutPrefab;
    public IPlantable MyPlant { get; set; } = null;

    public void Plant(PlantData plantData)
    {
        if (MyPlant != null)
        {
            Debug.LogWarning("�̹� �Ĺ��� �ɾ��� �ֽ��ϴ�.");
            return;
        }

        MyPlant = Instantiate(sproutPrefab, transform).GetComponent<IPlantable>();

        MonoBehaviour plantMono = MyPlant as MonoBehaviour;
        if (plantMono != null)
        {
            plantMono.transform.localPosition = Vector2.zero;
            MyPlant.plantData = plantData;
        }
    }
}
