using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Button upgradeButton;
    private ResourceManager resourceManager;
    private const int UPGRADE_COST = 100;

    void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager not found in the scene!");
        }

        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(UpgradeBuildings);
        }
    }

    void UpgradeBuildings()
    {
        if (resourceManager.SpendGold(UPGRADE_COST))
        {
            ResourceProducer[] producers = FindObjectsByType<ResourceProducer>(FindObjectsSortMode.None);
            foreach (ResourceProducer producer in producers)
            {
                producer.Upgrade();
            }
        }
    }
}