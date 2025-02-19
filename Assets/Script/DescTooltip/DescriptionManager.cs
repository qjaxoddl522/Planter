using TMPro;
using UnityEngine;

public class DescriptionManager : MonoBehaviour
{
    public static DescriptionManager Instance { get; private set; }

    [SerializeField] GameObject descriptionPanel;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;

    void Awake()
    {
        Instance = this;
    }

    public void ShowDescription(DescriptionData data)
    {
        titleText.text = data.title;
        descriptionText.text = data.description;
        descriptionPanel.SetActive(true);
    }

    public void HideDescription()
    {
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }
    }

    public bool isDescriptionActive()
    {
        return descriptionPanel.activeSelf;
    }
}
