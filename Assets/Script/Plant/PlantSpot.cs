using UnityEngine;

public interface IPlantSpot
{
    IPlantable MyPlant { get; set; }
    void Plant(Seed seedID);
}

public class PlantSpot : MonoBehaviour, IPlantSpot
{
    [SerializeField] GameObject sproutPrefab;
    public IPlantable MyPlant { get; set; } = null;

    public void Plant(Seed seedID)
    {
        if (MyPlant != null)
        {
            Debug.LogWarning("이미 식물이 심어져 있습니다.");
            return;
        }

        MyPlant = Instantiate(sproutPrefab, transform).GetComponent<IPlantable>();

        MonoBehaviour plantMono = MyPlant as MonoBehaviour;
        if (plantMono != null)
        {
            plantMono.transform.localPosition = Vector2.zero;
        }
    }
}
