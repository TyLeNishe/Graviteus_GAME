using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceTextManager : MonoBehaviour
{
    public TextMeshProUGUI obs_count, ign_count, ven_count;

    public void Update()
    {
        obs_count.text = ResourceDebug.storage["obs"].ToString();
        ign_count.text = ResourceDebug.storage["ign"].ToString();
        ven_count.text = ResourceDebug.storage["ven"].ToString();
    }
}

