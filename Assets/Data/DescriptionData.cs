using UnityEngine;

[CreateAssetMenu(fileName = "NewDescriptionData", menuName = "Scriptable Objects/Description Data", order = 51)]
public class DescriptionData : ScriptableObject
{
    public string title;
    [TextArea]
    public string description;
}