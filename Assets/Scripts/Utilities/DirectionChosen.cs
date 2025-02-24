using UnityEngine;

public class DirectionChosen : MonoBehaviour
{
    public static float hov_x = 0f, hov_y = 0f;
    public static float hov_x_cap_up = 1f, hov_y_cap_up = 1f, hov_x_cap_down = -1f, hov_y_cap_down = -1f, acceleration = 0.1f;

    public void UpHover_Chosen()
    {
        while (hov_y < hov_y_cap_up) { hov_y += acceleration; }    
    }
    public void DownHover_Chosen()
    {
        while (hov_y > hov_y_cap_down) { hov_y -= acceleration; }
    }
    public void RightHover_Chosen()
    {
        while (hov_x < hov_x_cap_up) { hov_x += acceleration; }
    }
    public void LeftHover_Chosen()
    {
        while (hov_x > hov_x_cap_down) { hov_x -= acceleration; }
    }

    public void Dir_UnChosen()
    {
        hov_x = 0f;
        hov_y = 0f;
    }

}
