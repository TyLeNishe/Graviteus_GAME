using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuotaScript : MonoBehaviour
{
    public Dictionary<string, float> quota =
    new Dictionary<string, float>();

    public Dictionary<string, int> weight =
    new Dictionary<string, int>();

    public float quota_difficulty;
    public string[] resourceID = {"obs", "ign", "ven", "val", "inst", "pug"}, chosen_resource = {"null", "null", "null"};

    public int packages_ready = 0;

    public bool launch = false;
    public Button LaunchButton;

    public TextMeshProUGUI res1_text, res2_text, res3_text;

    void Start()
    {   
        //Добавим в словарь каждый ресурс по его кодовому имени и его коэффициент сложности добычи

        quota.Add("obs", 0);
        quota.Add("ign", 0);
        quota.Add("ven", 0);
        quota.Add("val", 0);
        quota.Add("inst", 0);
        quota.Add("pug", 0);

        weight.Add("obs", 2);
        weight.Add("ign", 3);
        weight.Add("ven", 4);
        weight.Add("val", 13);
        weight.Add("inst", 14);
        weight.Add("pug", 60);
 
        NewQuota();
    }

    public void QuotaEnd()
    {
        for (int i = 0; i < resourceID.Length; i++)
        {
            ResourceDebug.storage[resourceID[i]] -= quota[resourceID[i]];
        }
        NewQuota();
    } 

    public void NewQuota()
    {
        quota_difficulty = Random.Range(1, 4);

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
                for (int j = 0; j < chosen_resource.Length; j++) { 
                    if (quota[chosen_resource[i]] == 0) { flag = true; } 
                    else { chosen_resource[i] = resourceID[Random.Range(0, 3)]; } 
                }
            }
            quota[chosen_resource[i]] = Mathf.Floor(Random.Range(((20 * (2 * Mathf.Pow(2, quota_difficulty))) / Mathf.Pow(weight[chosen_resource[i]], 0.9f)), ((20 * (2 * Mathf.Pow(2, quota_difficulty)) - 2) / Mathf.Pow(weight[chosen_resource[i]], 0.9f))));
        }
    }

    void Update()   

    {   if (quota_difficulty >= 1) { res1_text.text = chosen_resource[0] + ": " + quota[chosen_resource[0]].ToString(); }
        if (quota_difficulty >= 2) { res2_text.text = chosen_resource[1] + ": " + quota[chosen_resource[1]].ToString(); } else { res2_text.text = "==="; }
        if (quota_difficulty >= 3) { res3_text.text = chosen_resource[2] + ": " + quota[chosen_resource[2]].ToString(); } else { res3_text.text = "==="; }

        packages_ready = 0;
        for (int i = 0; i < resourceID.Length; i++)
        {
           if (quota[resourceID[i]] <= ResourceDebug.storage[resourceID[i]]) { packages_ready += 1;}
        }
        if (packages_ready >= resourceID.Length) { launch = true; } else { launch = false; }

        if (launch) { LaunchButton.interactable = true; } else { LaunchButton.interactable = false; }

    }
}
