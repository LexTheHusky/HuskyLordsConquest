using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public enum ResourceType { Gold, Wood, Food }
    public int gold = 540;
    public int wood = 300;
    public int food = 200;
    public int population = 0;
    public int maxPopulation = 0;
    private float populationTimer;

    void Start()
    {
        UpdateMaxPopulation();
    }

    void Update()
    {
        populationTimer += Time.deltaTime;
        if (populationTimer >= 5f)
        {
            populationTimer -= 5f;
            UpdatePopulation();
        }
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            Debug.Log($"Gold spent: {amount}. Remaining gold: {gold}");
            return true;
        }
        return false;
    }

    public bool SpendWood(int amount)
    {
        if (wood >= amount)
        {
            wood -= amount;
            Debug.Log($"Wood spent: {amount}. Remaining wood: {wood}");
            return true;
        }
        return false;
    }

    public bool SpendFood(int amount)
    {
        if (food >= amount)
        {
            food -= amount;
            Debug.Log($"Food spent: {amount}. Remaining food: {food}");
            UpdateMaxPopulation();
            return true;
        }
        return false;
    }

    void UpdateMaxPopulation()
    {
        maxPopulation = food / 10; // 10 food supports 1 population
        if (population > maxPopulation)
        {
            population = maxPopulation;
            Debug.Log($"Population reduced to {population} due to insufficient food.");
        }
    }

    void UpdatePopulation()
    {
        if (food >= population)
        {
            food -= population;
            Debug.Log($"Population consumed {population} food. Remaining food: {food}");
            UpdateMaxPopulation();

            if (population < maxPopulation)
            {
                population++;
                Debug.Log($"Population increased to {population}/{maxPopulation}");
            }
        }
        else
        {
            Debug.Log("Not enough food to sustain population!");
            population--;
            Debug.Log($"Population decreased to {population}/{maxPopulation}");
        }
    }
}