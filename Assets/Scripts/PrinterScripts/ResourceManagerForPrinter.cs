using UnityEngine;
using UnityEngine.UI;

public class PrinterResourceSystem : MonoBehaviour
{
    [Header("Основные ресурсы")]
    public Text[] resourceTexts; // 0:Тёмниум 1:Огнемасло 2:Тосид 3:Прочнит 4:Нестабилий 5:Пугнар
    public int[] resourcePrices;

    [Header("Конечные ресурсы")]
    public Text[] resourceTextsFinal;

    [Header("Система")]
    public Text totalText;
    public Button recycleButton;
    public Button sellButton;

    private int currentCartridges = 0;

    // Порядок должен совпадать с resourceTexts!
    private readonly string[] resourceNames =
        { "Тёмниум", "Огнемасло", "Токсид", "Прочнит", "Нестабилий", "Пугнар" };

    private int[] lastResourceValues;
    // private int lastTotal = 0;

    private void Start()
    {
        // Проверка настройки
        if (resourceTexts.Length != resourcePrices.Length)
        {
            Debug.LogError("Количество ресурсов и цен не совпадает");
            return;
        }

        lastResourceValues = new int[resourceTexts.Length];
        for (int i = 0; i < resourceTexts.Length; i++)
        {
            int.TryParse(resourceTexts[i].text, out lastResourceValues[i]);
        }

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

    private void CalculateCurrentTotal()
    {
        int newTotal = 0;
        for (int i = 0; i < resourceTexts.Length; i++)
        {
            if (int.TryParse(resourceTexts[i].text, out int quantity))
            {
                newTotal += quantity * resourcePrices[i];
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

            UpdateFinalResource(i, -currentAmount);

            gainedCartridges += currentAmount * resourcePrices[i];
            resourceTexts[i].text = "0";
            lastResourceValues[i] = 0;
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
            UpdateFinalResource("Картриджи", currentCartridges / 2);
            currentCartridges = 0;
            UpdateTotalDisplay();
        }
    }

    private void UpdateFinalResource(int resourceIndex, int amount)
    {
        if (resourceIndex >= 0 && resourceIndex < resourceNames.Length)
        {
            UpdateFinalResource(resourceNames[resourceIndex], amount);
        }
    }

    private void UpdateFinalResource(string resourceName, int amount)
    {
        foreach (var finalText in resourceTextsFinal)
        {
            if (finalText != null && finalText.text.StartsWith(resourceName))
            {
                string[] parts = finalText.text.Split(':');
                if (parts.Length > 1 && int.TryParse(parts[1].Trim(), out int currentValue))
                {
                    int newValue = Mathf.Max(0, currentValue + amount);
                    finalText.text = $"{resourceName}: {newValue}";
                    return;
                }
            }
        }
        Debug.LogError($"Ресурс '{resourceName}' не найден. Проверьте:");
        Debug.LogError("1. Соответствие имен в resourceNames и resourceTextsFinal");
        Debug.LogError("2. Что все ресурсы добавлены в массив resourceTextsFinal");
        Debug.LogError("3. Формат текста: 'имя: количество'");
    }

    private void UpdateTotalDisplay()
    {
        totalText.text = currentCartridges.ToString();
    }
}
