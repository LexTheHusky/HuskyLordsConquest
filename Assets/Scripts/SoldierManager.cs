using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    private ResourceManager resourceManager;

    void Start()
    {
        resourceManager = FindFirstObjectByType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager not found in the scene!");
        }
    }

    public void DeploySoldiers()
    {
        if (resourceManager == null) return;

        if (resourceManager.soldiers >= 1)
        {
            int soldiersToDeploy = resourceManager.soldiers;
            resourceManager.soldiers = 0;
            Debug.Log($"Deployed {soldiersToDeploy} soldiers!");
        }
        else
        {
            Debug.Log("No soldiers available to deploy!");
        }
    }
}