using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuotaScript : MonoBehaviour
{
    public Dictionary<string, float> quota =
    new Dictionary<string, float>();

    public Dictionary<string, int> weight =
    new Dictionary<string, int>();

    public float quota_difficulty;
    public string[] resourceID = { "obs", "ign", "ven", "val", "inst", "pug" }, chosen_resource = { "null", "null", "null" };

    public int packages_ready = 0, income = 0;

    public bool launch = false;
    public Button LaunchButton;

    public Text res1_text, res2_text, res3_text, income_text, LaunchButton_text;

    void Start()
    {
        //������� � ������� ������ ������ �� ��� �������� ����� � ��� ����������� ��������� ������

        quota.Add("obs", 0);
        quota.Add("ign", 0);
        quota.Add("ven", 0);
        quota.Add("val", 0);
        quota.Add("inst", 0);
        quota.Add("pug", 0);

        weight.Add("obs", 2);
        weight.Add("ign", 3);
        weight.Add("ven", 4);
        weight.Add("val", weight["obs"] * 5 + weight["ign"] * 5);
        weight.Add("inst", weight["ign"] * 15 + weight["ven"] * 10);
        weight.Add("pug", weight["val"] * 5 + weight["inst"] * 3);

        NewQuota();
    }

    public void QuotaEnd()
    {
        OxygenManager.oxygen += income;
        for (int i = 0; i < resourceID.Length; i++)
        {
            ResourceManager.resources[resourceID[i]] -= quota[resourceID[i]];
        }
        NewQuota();
    }

    public void NewQuota()
    {    
        quota_difficulty = Random.Range(1, 4);
        income = Mathf.FloorToInt(Random.Range(quota_difficulty + ((quota_difficulty - 1) * 10), quota_difficulty + 10 + ((quota_difficulty - 1) * 10)));

        for (int i = 0; i < resourceID.Length; i++)
        {
            quota[resourceID[i]] = 0;
        }

        for (int i = 0; i < quota_difficulty; i++)
        {
            bool flag = false;

            chosen_resource[i] = resourceID[Random.Range(0, 3)];
            while (!flag)
            {
                for (int j = 0; j < chosen_resource.Length; j++)
                {
                    if (quota[chosen_resource[i]] == 0) { flag = true; }
                    else { chosen_resource[i] = resourceID[Random.Range(0, 3)]; }
                }
            }

            float quota_increment = 10 * quota_difficulty;
            if (chosen_resource[i] == "obs") { quota[chosen_resource[i]] = Mathf.Floor(Random.Range(11 * quota_difficulty, 15 * quota_difficulty + 1)); }
            else if (chosen_resource[i] == "ign") { quota[chosen_resource[i]] = Mathf.Floor(Random.Range(12 * quota_difficulty, 16 * quota_difficulty + 1)); }
            else if (chosen_resource[i] == "ven") { quota[chosen_resource[i]] = Mathf.Floor(Random.Range(10 * quota_difficulty, 12 * quota_difficulty + 1)); }
            else if (chosen_resource[i] == "val") { quota[chosen_resource[i]] = Mathf.Floor(Random.Range(4 * quota_difficulty, 5 * quota_difficulty + 1)); }
            else if (chosen_resource[i] == "inst") { quota[chosen_resource[i]] = Mathf.Floor(Random.Range(2 * quota_difficulty, 3 * quota_difficulty + 1)); }
            else if (chosen_resource[i] == "pug") { quota[chosen_resource[i]] = Mathf.Floor(quota_difficulty); }
        }
    }

    void Update()
    {
        

        if (quota_difficulty >= 1) { res1_text.text = chosen_resource[0] + ": " + quota[chosen_resource[0]].ToString(); }
        if (quota_difficulty >= 2) { res2_text.text = chosen_resource[1] + ": " + quota[chosen_resource[1]].ToString(); } else { res2_text.text = ""; }
        if (quota_difficulty >= 3) { res3_text.text = chosen_resource[2] + ": " + quota[chosen_resource[2]].ToString(); } else { res3_text.text = ""; }
        income_text.text = "Награда: " + "+" + income.ToString() + " дней";
        packages_ready = 0;
        for (int i = 0; i < resourceID.Length; i++)
        {
            if (quota[resourceID[i]] <= ResourceManager.resources[resourceID[i]]) { packages_ready += 1; }
        }
        if (packages_ready >= resourceID.Length) { launch = true; } else { launch = false; }

        if (launch) { LaunchButton.interactable = true; LaunchButton_text.text = "Сдать квоту"; LaunchButton_text.color = new Color32(29, 201, 49, 255); } else { LaunchButton.interactable = false; LaunchButton_text.text = "Недостаточно ресуров!"; LaunchButton_text.color = new Color32(191, 7, 7, 255); }

        if (quota_difficulty >= 1)
        {
            if (quota[chosen_resource[0]] <= ResourceManager.resources[chosen_resource[0]]) { res1_text.color = new Color32(29, 201, 49, 255); } else { res1_text.color = new Color32(191, 7, 7, 255); }
        }
        if (quota_difficulty >= 2)
        {
            if (quota[chosen_resource[1]] <= ResourceManager.resources[chosen_resource[1]]) { res2_text.color = new Color32(29, 201, 49, 255); } else { res2_text.color = new Color32(191, 7, 7, 255); }
        }
        if (quota_difficulty >= 3)
        {
            if (quota[chosen_resource[2]] <= ResourceManager.resources[chosen_resource[2]]) { res3_text.color = new Color32(29, 201, 49, 255); } else { res3_text.color = new Color32(191, 7, 7, 255); }
        }

    }
}
