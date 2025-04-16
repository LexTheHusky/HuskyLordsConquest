using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int gold = 540;

    void Start()
    {
        Debug.Log("Starting Gold: " + gold);
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            Debug.Log("Gold spent: " + amount + ". Remaining gold: " + gold);
            return true;
        }
        else
        {
            Debug.Log("Not enough gold! Need " + amount + ", but only have " + gold);
            return false;
        }
    }
}