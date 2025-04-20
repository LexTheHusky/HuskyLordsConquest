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

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
        else
        {
            Debug.LogError("UpgradePanel is not assigned in BuildingSelector!");
        }

        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(UpgradeSelectedBuilding);
        }
        else
        {
            Debug.LogError("UpgradeButton is not assigned in BuildingSelector!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            Collider2D hit = Physics2D.OverlapPoint(mousePos2D);
            if (hit != null && (hit.CompareTag("GoldMine") || hit.CompareTag("LumberMill") || hit.CompareTag("Farm") || hit.CompareTag("TownHall") || hit.CompareTag("Barrack")))
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

    public void UpgradeSelectedBuilding() // Nu `public`
    {
        if (selectedBuilding == null)
        {
            Debug.Log("No building selected for upgrade!");
            return;
        }

        ResourceProducer producer = selectedBuilding.GetComponent<ResourceProducer>();
        if (producer != null)
        {
            if (producer.level >= producer.maxLevel)
            {
                Debug.Log("Building is already at max level!");
                return;
            }

            if (resourceManager.SpendGold(100))
            {
                producer.Upgrade();
                Debug.Log($"{selectedBuilding.name} upgraded to level {producer.level}. Now producing {producer.amountPerSecond} per second.");
            }
            else
            {
                Debug.Log("Not enough gold to upgrade!");
            }
            return;
        }

        Barrack barrack = selectedBuilding.GetComponent<Barrack>();
        if (barrack != null)
        {
            if (barrack.level >= barrack.maxLevel)
            {
                Debug.Log("Barrack is already at max level!");
                return;
            }

            if (resourceManager.SpendGold(100))
            {
                barrack.Upgrade();
                Debug.Log($"{selectedBuilding.name} upgraded to level {barrack.level}. Now producing {barrack.soldiersPerSecond * barrack.level} soldiers per second.");
            }
            else
            {
                Debug.Log("Not enough gold to upgrade!");
            }
            return;
        }

        Debug.LogError($"Selected building {selectedBuilding.name} does not have a ResourceProducer or Barrack component!");
    }
}