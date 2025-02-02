using System;
using UnityEngine;

public interface IPlantable
{
    Seed SeedID { get; set; }
}

public class Sprout : MonoBehaviour, IPlantable
{
    public Seed SeedID { get; set;}
    GrowthController growthController;

    void Awake()
    {
        growthController = gameObject.AddComponent<GrowthController>();
        growthController.growthDuration = 5f;
        growthController.OnGrowthComplete += HandleGrowthComplete;
    }

    private void HandleGrowthComplete()
    {
        Debug.Log("성장했어요.");
    }
}

public class GrowthController : MonoBehaviour
{
    public float growthDuration;
    private float elapsedTime;
    public event Action OnGrowthComplete;

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= growthDuration)
        {
            OnGrowthComplete?.Invoke();
            enabled = false;
        }
    }
}