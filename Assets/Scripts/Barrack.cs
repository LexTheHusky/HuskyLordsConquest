using UnityEngine;

public class Barrack : MonoBehaviour
{
    public int soldiersPerSecond = 1;
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
            ProduceSoldiers();
        }
    }

    void ProduceSoldiers()
    {
        if (resourceManager == null) return;

        int soldiersToProduce = soldiersPerSecond * level;
        resourceManager.AddSoldiers(soldiersToProduce);
        Debug.Log($"Barrack produced {soldiersToProduce} soldiers. Total soldiers: {resourceManager.soldiers}");
    }

    public void Upgrade()
    {
        if (level < maxLevel)
        {
            level++;
            Debug.Log($"Barrack upgraded to level {level}. Now producing {soldiersPerSecond * level} soldiers per second.");
        }
        else
        {
            Debug.Log($"Barrack is already at max level ({maxLevel})!");
        }
    }
}