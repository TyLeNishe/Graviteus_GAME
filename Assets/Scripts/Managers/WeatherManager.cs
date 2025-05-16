using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public static int weather_state = 0; //0 - ясна€ погода, 1 - ƒождь 2 - ...
    public GameObject rain;

    private void Update()
    {
        if (weather_state == 1) { rain.SetActive(!rain.activeSelf); }

        if (weather_state == 0 && Time_Script.weather_flag == true)
        {
            int weather_change_try = Random.Range(0, 100);

            if (weather_change_try <= 20) { weather_state = 1; }
            Time_Script.weather_flag = false;
        }
        else if (weather_state != 0)
        {
            int weather_change_try = Random.Range(0, 100);
            weather_change_try = Random.Range(0, 100);
            if (weather_change_try <= 80) { weather_state = 1; }
        }
    }              
    
}
