using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    public static float oxygen = 10f;
    public Text Oxygen_text, DaysLeft_text;
    void Update()
    {
        if (oxygen >= 1000f) { Oxygen_text.text = oxygen.ToString(); } 
        else if (oxygen >= 100f) { Oxygen_text.text = "0" + oxygen.ToString(); }
        else if (oxygen >= 10f) { Oxygen_text.text = "00" + oxygen.ToString(); }
        else if (oxygen >= 1f) { Oxygen_text.text = "000" + oxygen.ToString(); }

        if (oxygen > 10f) { Oxygen_text.color = new Color32(29, 201, 49, 255); DaysLeft_text.color = new Color32(29, 201, 49, 255); }
        else if (oxygen > 5f) { Oxygen_text.color = new Color32(255, 196, 0, 255); DaysLeft_text.color = new Color32(255, 196, 0, 255); }
        else { Oxygen_text.color = new Color32(191, 7, 7, 255); DaysLeft_text.color = new Color32(191, 7, 7, 255); }
    }
}
