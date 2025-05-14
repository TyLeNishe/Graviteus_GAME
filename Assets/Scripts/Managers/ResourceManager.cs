using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    // Ресурсы и их количество
    public static Dictionary<string, float> resources = new Dictionary<string, float>()
    {
        {"obs", 20},
        {"ign", 20},
        {"ven", 20},
        {"ctrg", 100},
        {"val", 6},
        {"inst", 7},
        {"pug", 1}
    };

    // Ссылки на текстовые элементы UI
    public Text obscurriumText;
    public Text ignoleumText;
    public Text venesumText;
    public Text cartridgesText;
    public Text valensiumText;
    public Text instabiliumText;
    public Text pugnarText;

    // Обновление всех текстовых полей
    void Update()
    {
        obscurriumText.text = "Тёмниум: " + resources["obs"].ToString();
        ignoleumText.text = "Огнемасло: " + resources["ign"].ToString();
        venesumText.text = "Токсид: " + resources["ven"].ToString();
        cartridgesText.text = "Картриджи: " + resources["ctrg"].ToString();
        valensiumText.text = "Прочнит: " + resources["val"].ToString();
        instabiliumText.text = "Нестабилий: " + resources["inst"].ToString();
        pugnarText.text = "Пугнар: " + resources["pug"].ToString();
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
                obscurriumText.text = "Тёмниум: " + resources[resourceName].ToString();
                break;
            case "ign":
                ignoleumText.text = "Огнемасло: " + resources[resourceName].ToString();
                break;
            case "ven":
                venesumText.text = "Токсид: " + resources[resourceName].ToString();
                break;
            case "ctrg":
                cartridgesText.text = "Картриджи: " + resources[resourceName].ToString();
                break;
            case "val":
                valensiumText.text = "Прочнит: " + resources[resourceName].ToString();
                break;
            case "inst":
                instabiliumText.text = "Нестабилий: " + resources[resourceName].ToString();
                break;
            case "pug":
                pugnarText.text = "Пугнар: " + resources[resourceName].ToString();
                break;
        }
    }
}