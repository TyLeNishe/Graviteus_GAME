using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    // Ресурсы и их количество
    public static Dictionary<string, float> resources = new()
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

    // Обновление всех текстовых полей
    public void Update()
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
        }
    }

    // Убрать ресурсы
    public bool RemoveResource(string resourceName, int amount)
    {
        if (resources.ContainsKey(resourceName) && resources[resourceName] >= amount)
        {
            resources[resourceName] -= amount;
            return true;
        }
        return false;
    }

    // Проверить наличие ресурсов
    public bool HasResource(string resourceName, int amount)
    {
        return resources.ContainsKey(resourceName) && resources[resourceName] >= amount;
    }

}