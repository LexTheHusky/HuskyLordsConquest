using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacement : MonoBehaviour
{
    public GameObject goldMinePrefab;
    public GameObject lumberMillPrefab;
    public GameObject farmPrefab;
    public int goldMineCost = 100;
    public int lumberMillCost = 50;
    public int farmCost = 20;
    public float tileSize = 1f;
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
        // Debug-logging om te controleren of de muis over een UI-element is
        if (EventSystem.current.IsPointerOverGameObject()) // Regel 27
        {
            Debug.Log("Mouse is over a UI element, skipping building placement.");
            return;
        }
        else
        {
            Debug.Log("Mouse is NOT over a UI element, proceeding with building placement.");
        }

        if (Input.GetMouseButtonDown(0)) // Left click for GoldMine
        {
            Debug.Log("Left mouse button clicked for GoldMine placement.");
            PlaceBuilding(goldMinePrefab, goldMineCost, ResourceManager.ResourceType.Gold);
        }
        if (Input.GetMouseButtonDown(1)) // Right click for LumberMill
        {
            Debug.Log("Right mouse button clicked for LumberMill placement.");
            PlaceBuilding(lumberMillPrefab, lumberMillCost, ResourceManager.ResourceType.Wood);
        }
        if (Input.GetKeyDown(KeyCode.F)) // F key for Farm
        {
            Debug.Log("F key pressed for Farm placement.");
            PlaceBuilding(farmPrefab, farmCost, ResourceManager.ResourceType.Food);
        }
    }

    void PlaceBuilding(GameObject buildingPrefab, int cost, ResourceManager.ResourceType costType)
    {
        if (buildingPrefab == null)
        {
            Debug.LogError("Building prefab is not assigned!");
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ensure z is 0 for 2D
        Debug.Log($"Mouse position in world space: {mousePos}");

        // Snap to grid
        mousePos.x = Mathf.Round(mousePos.x / tileSize) * tileSize;
        mousePos.y = Mathf.Round(mousePos.y / tileSize) * tileSize;
        Debug.Log($"Snapped position: {mousePos}");

        // Check for collisions (optional, to prevent overlap)
        Collider2D hit = Physics2D.OverlapPoint(mousePos);
        if (hit != null)
        {
            Debug.Log($"Cannot place building here, space is occupied by {hit.gameObject.name}!");
            return;
        }

        bool canAfford = false;
        if (costType == ResourceManager.ResourceType.Gold)
        {
            canAfford = resourceManager.SpendGold(cost);
        }
        else if (costType == ResourceManager.ResourceType.Wood)
        {
            canAfford = resourceManager.SpendWood(cost);
        }
        else if (costType == ResourceManager.ResourceType.Food)
        {
            canAfford = resourceManager.SpendFood(cost);
        }

        if (canAfford)
        {
            Instantiate(buildingPrefab, mousePos, Quaternion.identity);
            Debug.Log($"Placed {buildingPrefab.name} at position {mousePos}");
        }
        else
        {
            Debug.Log("Not enough resources to place building!");
        }
    }
}