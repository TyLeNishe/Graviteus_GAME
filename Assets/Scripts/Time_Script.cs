using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class Time_Script : MonoBehaviour
{
    public List<Sprite> Sprites;
    public Text Hours, Minutes, Day;

    private float min = 0, hour = 0, day = 0;
    private int clock_frame = 0;
    public Image clock;
    public static bool paused = false;

    void Update()
    {
        if (!paused)
        {
            if (clock_frame >= 32) { clock_frame = 0; }
            if (hour >= 24) { hour = 0; } else { hour += Time.deltaTime; }
            

            if (hour > (clock_frame) * 0.75f && hour <= (clock_frame + 1) * 0.75f) {  clock.sprite = Sprites[clock_frame]; clock_frame += 1; }

        }
        if (Mathf.FloorToInt(hour) < 10) { Hours.text = "0" + Mathf.FloorToInt(hour).ToString(); } else { Hours.text = Mathf.FloorToInt(hour).ToString(); }
       

    }          
}