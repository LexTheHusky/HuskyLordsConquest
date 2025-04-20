using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI populationText;
    public TextMeshProUGUI soldiersText; // Nieuwe TextMeshProUGUI voor soldaten
    private ResourceManager resourceManager;

    void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager not found in the scene!");
        }
    }

    void Update()
    {
        if (resourceManager != null)
        {
            goldText.text = "Gold: " + resourceManager.gold.ToString("F0"); // Regel 28
            woodText.text = "Wood: " + resourceManager.wood.ToString("F0"); // Regel 29
            foodText.text = "Food: " + resourceManager.food.ToString("F0"); // Regel 30
            populationText.text = "Population: " + resourceManager.population.ToString("F0"); // Regel 31
            soldiersText.text = "Soldiers: " + resourceManager.soldiers.ToString(); // Regel 32
        }
    }
}