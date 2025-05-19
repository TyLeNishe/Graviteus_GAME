using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public Texture2D[] animationTextures;
    public float frameRate = 0.5f;

    private Renderer[] sectorRenderers;
    private float timer;
    private int currentFrame;

    void Start()
    {
        sectorRenderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0;
            currentFrame = (currentFrame + 1) % animationTextures.Length;

            foreach (Renderer rend in sectorRenderers)
            {
                rend.material.mainTexture = animationTextures[currentFrame];
            }
        }
    }
}
