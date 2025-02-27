using UnityEngine;

public class WorldBorders : MonoBehaviour
{
    public static float North_Border = 10f;
    public static float South_Border = -18f;
    public static float West_Border = -13f;
    public static float East_Border = 13f;

    public static float Upper_Border = 20f;
    public static float Bottom_Border = 7f;

    public static float CamVelDist_Mult = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 CamPos = transform.position;

        //»«Ã≈Õ≈Õ»≈ — Œ–Œ—“»  ¿Ã≈–€ Œ“ –¿——“ŒﬂÕ»ﬂ ƒŒ  ¿–“€
        CamVelDist_Mult = (Mathf.Pow(CamPos.y, 0.7f) / 3);
    }
}
