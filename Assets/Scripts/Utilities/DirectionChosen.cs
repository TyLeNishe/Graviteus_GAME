using UnityEngine;

public class DirectionChosen : MonoBehaviour
{
    public static float hov_x = 0f, hov_z = 0f;
    public static float hov_x_cap_up = 1f, hov_z_cap_up = 1f, hov_x_cap_down = -1f, hov_z_cap_down = -1f, acceleration = 0.1f;

    public void UpHover_Chosen()
    {
        while (hov_z < hov_z_cap_up) { hov_z += acceleration; }
    }
    public void DownHover_Chosen()
    {
        while (hov_z > hov_z_cap_down) { hov_z -= acceleration; }
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
        hov_z = 0f;
    }

}
