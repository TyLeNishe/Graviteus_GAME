using UnityEngine;

public class RainManager : MonoBehaviour
{
    public GameObject rain;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            rain.SetActive(!rain.activeSelf);
        }
    }
}