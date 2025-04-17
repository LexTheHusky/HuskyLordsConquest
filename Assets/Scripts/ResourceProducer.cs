using UnityEngine;

public class ResourceProducer : MonoBehaviour
{
    public enum ResourceType { Gold, Wood, Food }
    public ResourceType resourceType;
    public int baseAmountPerSecond = 5;
    public int level = 1;
    public int maxLevel = 3;
    public int amountPerSecond;
    private ResourceManager resourceManager;
    private float timer;

    void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager not found in the scene!");
        }
        UpdateProductionRate();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer -= 1f;
            ProduceResource();
        }
    }

    void UpdateProductionRate()
    {
        amountPerSecond = baseAmountPerSecond * level;
    }

    public void Upgrade()
    {
        if (level < maxLevel)
        {
            level++;
            UpdateProductionRate();
            Debug.Log($"{resourceType} production upgraded to level {level}. Now producing {amountPerSecond} per second.");
        }
        else
        {
            Debug.Log($"{resourceType} production is already at max level ({maxLevel})!");
        }
    }

    void ProduceResource()
    {
        if (resourceManager == null) return;

        if (resourceType == ResourceType.Gold)
        {
            resourceManager.gold += amountPerSecond;
            Debug.Log($"Produced {amountPerSecond} gold. Total gold: {resourceManager.gold}");
        }
        else if (resourceType == ResourceType.Wood)
        {
            resourceManager.wood += amountPerSecond;
            Debug.Log($"Produced {amountPerSecond} wood. Total wood: {resourceManager.wood}");
        }
        else if (resourceType == ResourceType.Food)
        {
            resourceManager.food += amountPerSecond;
            Debug.Log($"Produced {amountPerSecond} food. Total food: {resourceManager.food}");
        }
    }
}