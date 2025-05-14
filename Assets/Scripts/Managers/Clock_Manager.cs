using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class Clock_Manager : MonoBehaviour
{
    public List<Sprite> Sprites;
    public SpriteRenderer spriteRenderer;
    public float dayDuration = 24f;
    public float activeStartSeconds = 3f;

    private float currentTime = 0f;
    public SpriteRenderer clock;



    private void Update()
    {
        currentTime += Time.deltaTime * (24f / dayDuration);
        if (currentTime >= 24f)
        {
            currentTime -= 24f;
        }


    }
}
