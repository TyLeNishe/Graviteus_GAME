using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Time_Script : MonoBehaviour
{
    public GameObject Minutes;
    public GameObject Hours;
    public GameObject Day;
    private int min = 0;
    private int hour = 0;
    private int day = 0;

    void Start()
    {
        StartCoroutine("GameTimer", 1);
    }

    IEnumerator GameTimer(float waitTime)
    {
        while (true)
        {
            {
                hour += 1;
                if (hour <= 9) Hours.GetComponent<Text>().text = "0" + hour.ToString();
                else if (hour < 24) Hours.GetComponent<Text>().text = hour.ToString();
                else
                {
                    day += 1;
                    hour = 0;
                    min = 0;

                    if (day <= 9) Day.GetComponent<Text>().text = "0" + day.ToString();
                    else Day.GetComponent<Text>().text = day.ToString();
                }

                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}