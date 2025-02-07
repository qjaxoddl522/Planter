using UnityEngine;

public abstract class PlantBase : MonoBehaviour
{
    public abstract PlantData data { get; set; }
    
    public abstract void Ability();
}
