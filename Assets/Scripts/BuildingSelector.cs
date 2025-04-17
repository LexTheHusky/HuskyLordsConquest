using UnityEngine;
using UnityEngine.UI;

public class BuildingSelector : MonoBehaviour
{
    public GameObject upgradePanel;
    public Button upgradeButton;
    private GameObject selectedBuilding;
    private ResourceManager resourceManager;

    void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager not found in the scene!");
        }

        upgradePanel.SetActive(false);
        upgradeButton.onClick.AddListener(UpgradeSelectedBuilding);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            Collider2D hit = Physics2D.OverlapPoint(mousePos2D);
            if (hit != null && (hit.CompareTag("GoldMine") || hit.CompareTag("LumberMill") || hit.CompareTag("Farm") || hit.CompareTag("TownHall")))
            {
                selectedBuilding = hit.gameObject;
                upgradePanel.SetActive(true);
                Debug.Log($"Selected building: {selectedBuilding.name}");
            }
            else
            {
                selectedBuilding = null;
                upgradePanel.SetActive(false);
            }
        }
    }

    void UpgradeSelectedBuilding()
    {
        if (selectedBuilding == null)
        {
            Debug.Log("No building selected for upgrade!");
            return;
        }

        ResourceProducer producer = selectedBuilding.GetComponent<ResourceProducer>();
        if (producer == null)
        {
            Debug.LogError("Selected building does not have a ResourceProducer component!");
            return;
        }

        if (producer.level >= producer.maxLevel) // Gebruik maxLevel uit ResourceProducer
        {
            Debug.Log("Building is already at max level!");
            return;
        }

        if (resourceManager.SpendGold(100))
        {
            producer.Upgrade(); // Gebruik de Upgrade-methode van ResourceProducer
            Debug.Log($"{selectedBuilding.name} upgraded to level {producer.level}. Now producing {producer.amountPerSecond} per second.");
        }
        else
        {
            Debug.Log("Not enough gold to upgrade!");
        }
    }
}
    
