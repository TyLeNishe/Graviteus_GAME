using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CraftingSystem : MonoBehaviour
{
    [System.Serializable]
    public class CraftingRecipe
    {
        [Tooltip("Ключ результата")]
        public string resultItemKey;
        public int resultAmount = 1;

        [Tooltip("Необходимые ресурсы")]
        public List<RequiredResource> requiredResources;
    }

    [System.Serializable]
    public class RequiredResource
    {
        [Tooltip("Ключ ресурса")]
        public string resourceKey; 
        public int amount;
    }

    public CraftingRecipe recipe;
    private Button craftButton;

    void Start()
    {
        craftButton = GetComponent<Button>();
        if (craftButton != null)
        {
            craftButton.onClick.AddListener(TryCraft);
        }
    }

    public void TryCraft()
    {
        if (CanCraft())
        {
            Craft();
            Debug.Log($"Создано: {recipe.resultAmount} {recipe.resultItemKey}");
        }
        else
        {
            Debug.Log("Недостаточно ресурсов!");
        }
    }

    private bool CanCraft()
    {
        foreach (var req in recipe.requiredResources)
        {
            if (!ResourceManager.Instance.HasResource(req.resourceKey, req.amount))
                return false;
        }
        return true;
    }

    private void Craft()
    {
        foreach (var req in recipe.requiredResources)
        {
            ResourceManager.Instance.RemoveResource(req.resourceKey, req.amount);
        }
        ResourceManager.Instance.AddResource(recipe.resultItemKey, recipe.resultAmount);
    }
}