using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    // Ресурсы и их количество
    public Dictionary<string, int> resources = new Dictionary<string, int>()
    {
        {"Тёмниум", 100},
        {"Огнемасло", 100},
        {"Токсид", 100},
        {"Картриджи", 100},
        {"Прочнит", 100},
        {"Нестабилий", 100},
        {"Путнар", 100}
    };

    // Ссылки на текстовые элементы UI
    public Text darkiumText;
    public Text fireoilText;
    public Text toxidText;
    public Text cartridgesText;
    public Text prochnitText;
    public Text unstableText;
    public Text putnarText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateAllUI();
    }

    // Обновление всех текстовых полей
    public void UpdateAllUI()
    {
        darkiumText.text = "Тёмниум: " + resources["Тёмниум"].ToString();
        fireoilText.text = "Огнемасло: " + resources["Огнемасло"].ToString();
        toxidText.text = "Токсид: " + resources["Токсид"].ToString();
        cartridgesText.text = "Картриджи: " + resources["Картриджи"].ToString();
        prochnitText.text = "Прочнит: " + resources["Прочнит"].ToString();
        unstableText.text = "Нестабилий: " + resources["Нестабилий"].ToString();
        putnarText.text = "Путнар: " + resources["Путнар"].ToString();
    }

    // Добавить ресурсы
    public void AddResource(string resourceName, int amount)
    {
        if (resources.ContainsKey(resourceName))
        {
            resources[resourceName] += amount;
            UpdateUI(resourceName);
        }
    }

    // Убрать ресурсы
    public bool RemoveResource(string resourceName, int amount)
    {
        if (resources.ContainsKey(resourceName) && resources[resourceName] >= amount)
        {
            resources[resourceName] -= amount;
            UpdateUI(resourceName);
            return true;
        }
        return false;
    }

    // Проверить наличие ресурсов
    public bool HasResource(string resourceName, int amount)
    {
        return resources.ContainsKey(resourceName) && resources[resourceName] >= amount;
    }

    // Обновление конкретного текстового поля
    private void UpdateUI(string resourceName)
    {
        switch (resourceName)
        {
            case "Тёмниум":
                darkiumText.text = "Тёмниум: " + resources[resourceName].ToString();
                break;
            case "Огнемасло":
                fireoilText.text = "Огнемасло: " + resources[resourceName].ToString();
                break;
            case "Токсид":
                toxidText.text = "Токсид: " + resources[resourceName].ToString();
                break;
            case "Картриджи":
                cartridgesText.text = "Картриджи: " + resources[resourceName].ToString();
                break;
            case "Прочнит":
                prochnitText.text = "Прочнит: " + resources[resourceName].ToString();
                break;
            case "Нестабилий":
                unstableText.text = "Нестабилий: " + resources[resourceName].ToString();
                break;
            case "Путнар":
                putnarText.text = "Путнар: " + resources[resourceName].ToString();
                break;
        }
    }
}