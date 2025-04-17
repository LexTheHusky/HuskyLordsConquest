using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacement : MonoBehaviour
{
    public GameObject townHallPrefab;
    public GameObject goldMinePrefab;
    public GameObject lumberMillPrefab;
    public GameObject farmPrefab;
    public int townHallCost = 100;
    public int goldMineCost = 100;
    public int lumberMillCost = 50;
    public int farmCost = 20;
    public float tileSize = 1f;
    private ResourceManager resourceManager;
    private GameObject selectedBuildingPrefab;
    private int selectedBuildingCost;
    private ResourceManager.ResourceType selectedCostType;
    private bool hasPlacedTownHall = false;

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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (selectedBuildingPrefab != null && Input.GetMouseButtonDown(0))
        {
            PlaceBuilding();
        }
    }

    public void SelectBuilding(string buildingType)
    {
        // Alleen andere gebouwen selecteren als TownHall is geplaatst
        if (!hasPlacedTownHall && buildingType != "TownHall")
        {
            Debug.Log("You must place a TownHall first!");
            return;
        }

        switch (buildingType)
        {
            case "TownHall":
                selectedBuildingPrefab = townHallPrefab;
                selectedBuildingCost = townHallCost;
                selectedCostType = ResourceManager.ResourceType.Gold;
                break;
            case "GoldMine":
                selectedBuildingPrefab = goldMinePrefab;
                selectedBuildingCost = goldMineCost;
                selectedCostType = ResourceManager.ResourceType.Gold;
                break;
            case "LumberMill":
                selectedBuildingPrefab = lumberMillPrefab;
                selectedBuildingCost = lumberMillCost;
                selectedCostType = ResourceManager.ResourceType.Wood;
                break;
            case "Farm":
                selectedBuildingPrefab = farmPrefab;
                selectedBuildingCost = farmCost;
                selectedCostType = ResourceManager.ResourceType.Food;
                break;
            default:
                selectedBuildingPrefab = null;
                selectedBuildingCost = 0;
                selectedCostType = ResourceManager.ResourceType.Gold;
                break;
        }
        Debug.Log($"Selected building: {buildingType}");
    }

    void PlaceBuilding()
    {
        if (selectedBuildingPrefab == null)
        {
            Debug.Log("No building selected!");
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        mousePos.x = Mathf.Round(mousePos.x / tileSize) * tileSize;
        mousePos.y = Mathf.Round(mousePos.y / tileSize) * tileSize;

        Collider2D hit = Physics2D.OverlapPoint(mousePos);
        if (hit != null)
        {
            Debug.Log("Cannot place building here, space is occupied!");
            return;
        }

        bool canAfford = false;
        if (selectedCostType == ResourceManager.ResourceType.Gold)
        {
            canAfford = resourceManager.SpendGold(selectedBuildingCost);
        }
        else if (selectedCostType == ResourceManager.ResourceType.Wood)
        {
            canAfford = resourceManager.SpendWood(selectedBuildingCost);
        }
        else if (selectedCostType == ResourceManager.ResourceType.Food)
        {
            canAfford = resourceManager.SpendFood(selectedBuildingCost);
        }

        if (canAfford)
        {
            Instantiate(selectedBuildingPrefab, mousePos, Quaternion.identity);
            Debug.Log($"Placed {selectedBuildingPrefab.name} at position {mousePos}");

            if (selectedBuildingPrefab == townHallPrefab)
            {
                hasPlacedTownHall = true;
            }
        }
        else
        {
            Debug.Log("Not enough resources to place building!");
        }
    }
}