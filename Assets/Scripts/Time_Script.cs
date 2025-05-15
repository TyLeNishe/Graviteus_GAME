using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
public class Time_Script : MonoBehaviour
{
    public List<Sprite> Active_Sprites, Inactive_Sprites;
    public Text Hours, Minutes, Day;

    private float min = 0, hour = 0, day = 0, full_circles = 8, current_circle = 0, time_speed = 1;
    private int clock_frame = 1;
    public Image clock;
    public static bool paused = false;


    public void X05_speed() 
    {
        time_speed = 0.5f;      
    }
    public void X1_speed() 
    {
        time_speed = 1f;
    }
    public void X2_speed() 
    {
        time_speed = 2f;
    }
    public void X4_speed() 
    {
        time_speed = 4f;
    }

    void Update()
    {
        if (!paused)
        {
            if (clock_frame >= 17) { clock_frame = 1; current_circle += 1; }

            if (current_circle >= full_circles) { current_circle = 0; }

            if (hour >= 24f) { hour = 0.0f; } else { hour += Time.deltaTime * time_speed; }

            if (hour > (clock_frame + (current_circle * 16)) * (24f / 16f / full_circles) && hour <= (clock_frame + 1 + (current_circle * 16)) * (24f / 16f / full_circles)) { clock.sprite = Inactive_Sprites[clock_frame - 1]; clock_frame += 1; }
           
        }
        if (Mathf.FloorToInt(hour) < 10) { Hours.text = "0" + Mathf.FloorToInt(hour).ToString(); } else { Hours.text = Mathf.FloorToInt(hour).ToString(); }
       
    }          


}