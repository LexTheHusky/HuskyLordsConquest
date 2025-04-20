using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public float gold = 540f;
    public float wood = 300f;
    public float food = 200f;
    public int population;
    public int soldiers;

    public void AddGold(float amount)
    {
        gold += amount;
    }

    public void AddWood(float amount)
    {
        wood += amount;
    }

    public void AddFood(float amount)
    {
        food += amount;
    }

    public void AddSoldiers(int amount)
    {
        soldiers += amount;
    }

    public bool SpendGold(float amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        return false;
    }

    public bool SpendWood(float amount)
    {
        if (wood >= amount)
        {
            wood -= amount;
            return true;
        }
        return false;
    }

    public bool SpendFood(float amount)
    {
        if (food >= amount)
        {
            food -= amount;
            return true;
        }
        return false;
    }
}