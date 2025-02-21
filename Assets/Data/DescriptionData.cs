using UnityEngine;

[CreateAssetMenu(fileName = "NewDescriptionData", menuName = "Scriptable Objects/Description Data")]
public class DescriptionData : ScriptableObject
{
    public string title;
    [TextArea]
    public string description;
}