using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    // Ресурсы и их количество
    public Dictionary<string, int> resources = new Dictionary<string, int>()
    {
        {"obs", 20},
        {"ign", 20},
        {"ven", 20},
        {"val", 6},
        {"inst", 7},
        {"pug", 1},
        {"crtg", 100},
    };

    // Ссылки на текстовые элементы UI
    public Text darkiumText;
    public Text fireoilText;
    public Text toxidText;
    public Text prochnitText;
    public Text unstableText;
    public Text putnarText;
    public Text cartridgesText;

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
        darkiumText.text = "Тёмниум: " + resources["obs"].ToString();
        fireoilText.text = "Огнемасло: " + resources["ign"].ToString();
        toxidText.text = "Токсид: " + resources["ven"].ToString();
        prochnitText.text = "Прочнит: " + resources["val"].ToString();
        unstableText.text = "Нестабилий: " + resources["inst"].ToString();
        putnarText.text = "Пугнар: " + resources["pug"].ToString();
        cartridgesText.text = "Картриджи: " + resources["crtg"].ToString();
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
            case "obs":
                darkiumText.text = "Тёмниум: " + resources[resourceName].ToString();
                break;
            case "ign":
                fireoilText.text = "Огнемасло: " + resources[resourceName].ToString();
                break;
            case "ven":
                toxidText.text = "Токсид: " + resources[resourceName].ToString();
                break;
            case "val":
                prochnitText.text = "Прочнит: " + resources[resourceName].ToString();
                break;
            case "inst":
                unstableText.text = "Нестабилий: " + resources[resourceName].ToString();
                break;
            case "pug":
                putnarText.text = "Путнар: " + resources[resourceName].ToString();
                break;
            case "crtg":
                cartridgesText.text = "Картриджи: " + resources[resourceName].ToString();
                break;
        }
    }
}