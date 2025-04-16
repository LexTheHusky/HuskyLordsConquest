using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI populationText;
    private ResourceManager resourceManager;

    void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager not found in the scene!");
        }
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (resourceManager != null)
        {
            goldText.text = "Gold: " + resourceManager.gold;
            woodText.text = "Wood: " + resourceManager.wood;
            foodText.text = "Food: " + resourceManager.food;
            populationText.text = "Population: " + resourceManager.population + "/" + resourceManager.maxPopulation;
        }
    }
}