using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public static int weather_state = 0, fog_state = 0;
    public GameObject rain;
    [SerializeField] private float maxFogDensity = 0.06f;
    [SerializeField] private float fogChangeSpeed = 0.5f;

    private void Update()
    {
        rain.SetActive(weather_state == 1);

        UpdateFog();

        if (Time_Script.weather_flag)
        {
            ChangeWeather();
            Time_Script.weather_flag = false;
        }

        if (Time_Script.fog_flag)
        {
            ChangeFog();
            Time_Script.fog_flag = false;
        }
    }

    private void UpdateFog()
    {
        float targetDensity = fog_state == 0 ? 0f : maxFogDensity;

        RenderSettings.fogDensity = Mathf.Lerp(
            RenderSettings.fogDensity,
            targetDensity,
            Time.deltaTime * fogChangeSpeed
        );

        RenderSettings.fogDensity = Mathf.Clamp(RenderSettings.fogDensity, 0f, maxFogDensity);

        RenderSettings.fog = RenderSettings.fogDensity > 0.001f;
    }

    private void ChangeWeather()
    {
        if (weather_state == 0)
        {
            weather_state = Random.Range(0, 100) <= 30 ? 1 : 0;
        }
        else
        {
            weather_state = Random.Range(0, 100) <= 60 ? 0 : weather_state;
        }
    }

    private void ChangeFog()
    {
        if (RenderSettings.fogDensity > 0.001f)
        {
            fog_state = Random.Range(0, 100) <= 15 ? 1 : 0;
        }
        else
        {
            fog_state = Random.Range(0, 100) <= 85 ? 0 : 1;
        }
    }
}