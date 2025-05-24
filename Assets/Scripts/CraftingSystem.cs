using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    [System.Serializable]
    public class CraftingRecipe
    {
        public string resultItem;
        public int resultAmount = 1;

        public List<RequiredResource> requiredResources;
    }

    [System.Serializable]
    public class RequiredResource
    {
        public string resourceName;
        public int amount;
    }

    public CraftingRecipe recipe;

    private Button craftButton;

    void Start()
    {
        craftButton = GetComponent<Button>();
        craftButton.onClick.AddListener(CraftItem);
    }

    public void CraftItem()
    {
        if (HasRequiredResources())
        {
            foreach (var req in recipe.requiredResources)
            {
                ResourceManager.Instance.RemoveResource(req.resourceName, req.amount);
            }

            ResourceManager.Instance.AddResource(recipe.resultItem, recipe.resultAmount);

            Debug.Log($"Успешно создано: {recipe.resultAmount} {recipe.resultItem}!");
        }
        else
        {
            Debug.Log("Недостаточно ресурсов!");
        }
    }

    private bool HasRequiredResources()
    {
        foreach (var req in recipe.requiredResources)
        {
            if (!ResourceManager.Instance.HasResource(req.resourceName, req.amount))
                return false;
        }
        return true;
    }
}