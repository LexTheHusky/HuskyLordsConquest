using UnityEngine;
using System.Linq;

public class BuildingPlacement : MonoBehaviour
{
    public GameObject townHallPrefab;
    public GameObject goldMinePrefab;
    public GameObject lumberMillPrefab;
    public GameObject farmPrefab;
    public GameObject barrackPrefab;
    public float tileSize = 1f;
    private GameObject selectedBuildingPrefab;
    private ResourceManager resourceManager;

    void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager not found in the scene!");
        }
    }

    public void SelectBuilding(GameObject buildingPrefab)
    {
        selectedBuildingPrefab = buildingPrefab;
        Debug.Log($"Selected building to place: {buildingPrefab?.name ?? "None"}");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selectedBuildingPrefab != null)
        {
            PlaceBuilding();
        }
    }

    void PlaceBuilding()
    {
        Debug.Log("Attempting to place building...");
        if (selectedBuildingPrefab == null)
        {
            Debug.Log("No building selected!");
            return;
        }

        Debug.Log($"Selected building: {selectedBuildingPrefab.name}, Tag: {selectedBuildingPrefab.tag}");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        mousePos.x = Mathf.Round(mousePos.x / tileSize) * tileSize;
        mousePos.y = Mathf.Round(mousePos.y / tileSize) * tileSize;
        Debug.Log($"Mouse position: {mousePos}");

        Collider2D hit = Physics2D.OverlapPoint(mousePos);
        if (hit != null)
        {
            Debug.Log($"Cannot place building here, space is occupied! Hit: {hit.gameObject.name}");
            return;
        }

        bool hasTownHall = FindObjectsOfType<GameObject>().Any(obj => obj.CompareTag("TownHall"));
        if (!hasTownHall && selectedBuildingPrefab.CompareTag("TownHall"))
        {
            hasTownHall = true;
        }

        if (!hasTownHall)
        {
            Debug.Log("You must place a TownHall first!");
            return;
        }

        Debug.Log($"Resources - Gold: {resourceManager.gold}, Wood: {resourceManager.wood}, Food: {resourceManager.food}");

        if (selectedBuildingPrefab.CompareTag("TownHall") && resourceManager.SpendGold(100))
        {
            Instantiate(selectedBuildingPrefab, mousePos, Quaternion.identity);
            Debug.Log($"Placed TownHall at position {mousePos}");
        }
        else if (selectedBuildingPrefab.CompareTag("GoldMine") && resourceManager.SpendGold(100))
        {
            Instantiate(selectedBuildingPrefab, mousePos, Quaternion.identity);
            resourceManager.population += 1;
            Debug.Log($"Placed GoldMine at position {mousePos}");
        }
        else if (selectedBuildingPrefab.CompareTag("LumberMill") && resourceManager.SpendWood(50))
        {
            Instantiate(selectedBuildingPrefab, mousePos, Quaternion.identity);
            resourceManager.population += 1;
            Debug.Log($"Placed LumberMill at position {mousePos}");
        }
        else if (selectedBuildingPrefab.CompareTag("Farm") && resourceManager.SpendFood(20))
        {
            Instantiate(selectedBuildingPrefab, mousePos, Quaternion.identity);
            resourceManager.population += 1;
            Debug.Log($"Placed Farm at position {mousePos}");
        }
        else if (selectedBuildingPrefab.CompareTag("Barrack") && resourceManager.SpendFood(50))
        {
            Instantiate(selectedBuildingPrefab, mousePos, Quaternion.identity);
            resourceManager.population += 1;
            Debug.Log($"Placed Barrack at position {mousePos}");
        }
        else
        {
            Debug.Log("Not enough resources to place this building!");
        }

        selectedBuildingPrefab = null;
    }
}