using UnityEngine;

public class ResourceProducer : MonoBehaviour
{
    public float amountPerSecond = 1f;
    public int level = 1;
    public int maxLevel = 3;
    private float timer;
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
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer -= 1f;
            ProduceResources();
        }
    }

    void ProduceResources()
    {
        if (resourceManager == null) return;

        float amountToProduce = amountPerSecond * level;
        if (gameObject.CompareTag("GoldMine"))
        {
            resourceManager.AddGold(amountToProduce);
            Debug.Log($"GoldMine produced {amountToProduce} gold. Total gold: {resourceManager.gold}");
        }
        else if (gameObject.CompareTag("LumberMill"))
        {
            resourceManager.AddWood(amountToProduce);
            Debug.Log($"LumberMill produced {amountToProduce} wood. Total wood: {resourceManager.wood}");
        }
        else if (gameObject.CompareTag("Farm"))
        {
            resourceManager.AddFood(amountToProduce);
            Debug.Log($"Farm produced {amountToProduce} food. Total food: {resourceManager.food}");
        }
        else if (gameObject.CompareTag("TownHall"))
        {
            resourceManager.AddGold(amountToProduce);
            Debug.Log($"TownHall produced {amountToProduce} gold. Total gold: {resourceManager.gold}");
        }
    }

    public void Upgrade()
    {
        if (level < maxLevel)
        {
            level++;
            Debug.Log($"Upgraded {gameObject.name} to level {level}. Now producing {amountPerSecond * level} per second.");
        }
    }
}