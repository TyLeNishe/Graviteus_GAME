using System.Collections.Generic;
using UnityEngine;

public class Clock_Manager : MonoBehaviour
{
    public List<Sprite> inactiveSprites;
    public List<Sprite> activeSprites;
    public SpriteRenderer spriteRenderer;
    public float dayDuration = 24f;
    public float activeStartSeconds = 3f;

    private float currentTime = 0f;
    private bool isActive = false;
    private float frameTimer = 0f;
    private int currentFrame = 0;

    private void Update()
    {
        currentTime += Time.deltaTime * (24f / dayDuration);
        if (currentTime >= 24f)
        {
            currentTime -= 24f;
        }

        float timeLeft = 24f - currentTime;
        bool shouldBeActive = timeLeft <= activeStartSeconds;

        if (shouldBeActive != isActive)
        {
            isActive = shouldBeActive;
            currentFrame = 0;
            frameTimer = 0f;
        }

        List<Sprite> currentSprites = isActive ? activeSprites : inactiveSprites;
        int frameCount = currentSprites.Count;
        float availableTime = isActive ? activeStartSeconds : (24f - activeStartSeconds);
        float frameDuration = availableTime / frameCount;

        frameTimer += Time.deltaTime;
        if (frameTimer >= frameDuration)
        {
            frameTimer -= frameDuration;
            currentFrame = (currentFrame + 1) % frameCount;
            spriteRenderer.sprite = currentSprites[currentFrame];
        }
    }
}
