using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class Time_Script : MonoBehaviour
{
    public List<Sprite> Active_Sprites, Inactive_Sprites;
    public Text Hours, Minutes, Day;

    private float min = 0, hour = 0, day = 0, full_circles = 16, current_circle = 0;
    private int clock_frame = 1;
    public Image clock;
    public static bool paused = false;

    void Update()
    {
        if (!paused)
        {
            if (clock_frame >= 17) { clock_frame = 1; current_circle += 1; }

            if (current_circle >= full_circles) { current_circle = 0; }

            if (hour >= 24f) { hour = 0.0f; } else { hour += Time.deltaTime; }

            if (hour > (clock_frame + (current_circle * 16)) * (24f / 16f / full_circles) && hour <= (clock_frame + 1 + (current_circle * 16)) * (24f / 16f / full_circles)) { clock.sprite = Inactive_Sprites[clock_frame - 1]; clock_frame += 1; }
           
        }
        if (Mathf.FloorToInt(hour) < 10) { Hours.text = "0" + Mathf.FloorToInt(hour).ToString(); } else { Hours.text = Mathf.FloorToInt(hour).ToString(); }
       

    }          
}