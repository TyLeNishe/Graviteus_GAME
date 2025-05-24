using UnityEngine;
using UnityEngine.UI;

public class PrinterResourceSystem : MonoBehaviour
{
    [System.Serializable]
    public class ResourceMapping
    {
        public string displayName;
        public string resourceKey;
        public int price;
    }

    [Header("Настройки ресурсов")]
    public ResourceMapping[] resourceMappings;

    [Header("UI элементы")]
    public Text[] resourceTexts;
    public Text[] resourceTextsFinal;
    public Text totalText;
    public Button recycleButton;
    public Button sellButton;

    private int currentCartridges = 0;
    private int[] lastResourceValues;

    private void Start()
    {
        if (resourceTexts.Length != resourceMappings.Length)
        {
            Debug.LogError("Количество ресурсов не совпадает с настройками!");
            return;
        }

        lastResourceValues = new int[resourceTexts.Length];
        RefreshResourceValues();

        recycleButton.onClick.AddListener(ProcessResources);
        sellButton.onClick.AddListener(SellCartridges);
        UpdateTotalDisplay();
    }

    private void Update()
    {
        for (int i = 0; i < resourceTexts.Length; i++)
        {
            if (int.TryParse(resourceTexts[i].text, out int currentValue) &&
                currentValue != lastResourceValues[i])
            {
                lastResourceValues[i] = currentValue;
                CalculateCurrentTotal();
                break;
            }
        }
    }

    private void RefreshResourceValues()
    {
        for (int i = 0; i < resourceTexts.Length; i++)
        {
            int.TryParse(resourceTexts[i].text, out lastResourceValues[i]);
        }
    }

    private void CalculateCurrentTotal()
    {
        int newTotal = 0;
        for (int i = 0; i < resourceTexts.Length; i++)
        {
            if (int.TryParse(resourceTexts[i].text, out int quantity))
            {
                newTotal += quantity * resourceMappings[i].price;
            }
        }

        if (newTotal != currentCartridges)
        {
            currentCartridges = newTotal;
            UpdateTotalDisplay();
        }
    }

    public void ProcessResources()
    {
        int gainedCartridges = 0;

        for (int i = 0; i < resourceTexts.Length; i++)
        {
            if (!int.TryParse(resourceTexts[i].text, out int currentAmount) || currentAmount <= 0)
                continue;

            // Обновляем через ResourceManager
            if (ResourceManager.Instance.RemoveResource(resourceMappings[i].resourceKey, currentAmount))
            {
                gainedCartridges += currentAmount * resourceMappings[i].price;
                resourceTexts[i].text = "0";
                lastResourceValues[i] = 0;
            }
        }

        if (gainedCartridges > 0)
        {
            currentCartridges += gainedCartridges;
            UpdateTotalDisplay();
        }
    }

    public void SellCartridges()
    {
        if (currentCartridges > 0)
        {
            ResourceManager.Instance.AddResource("crtg", currentCartridges / 2);
            currentCartridges = 0;
            UpdateTotalDisplay();
            UpdateFinalResourcesUI();
        }
    }

    private void UpdateFinalResourcesUI()
    {
        foreach (var finalText in resourceTextsFinal)
        {
            if (finalText.text.Contains("Картриджи"))
            {
                finalText.text = $"Картриджи: {ResourceManager.Instance.GetResourceAmount("crtg")}";
                break;
            }
        }
    }

    private void UpdateTotalDisplay()
    {
        totalText.text = currentCartridges.ToString();
    }
}