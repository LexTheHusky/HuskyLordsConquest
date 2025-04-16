using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingPlacement : MonoBehaviour
{
    public GameObject goldMinePrefab; // Koppel je Goudmijn-prefab in de Inspector
    public Tilemap tilemap;
    private ResourceManager resourceManager;

    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager not found in the scene!");
        }
        if (tilemap == null)
        {
            Debug.LogError("Tilemap not assigned in BuildingPlacement!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Klik met de linker muisknop om te plaatsen
        {
            PlaceBuilding();
        }
    }

    void PlaceBuilding()
    {
        // Converteer muispositie naar wereldpositie
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mousePos);

        // Controleer of de speler genoeg goud heeft (100 Goud voor een Goudmijn)
        if (resourceManager.SpendGold(100))
        {
            // Plaats de Goudmijn op de Tilemap-positie
            Vector3 spawnPos = tilemap.GetCellCenterWorld(cellPos);
            spawnPos.z = 0; // Zorg ervoor dat het gebouw op de juiste Z-positie staat
            Instantiate(goldMinePrefab, spawnPos, Quaternion.identity);
        }
    }
}