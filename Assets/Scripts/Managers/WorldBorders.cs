using UnityEngine;

public class WorldBorders : MonoBehaviour
{
    public static float cell_width = 1.4f;   

    public static float North_Border = cell_width * HexagonGeneration.layers - cell_width * 5;
    public static float South_Border = -cell_width * HexagonGeneration.layers - cell_width * 3;
    public static float West_Border = -cell_width * HexagonGeneration.layers;
    public static float East_Border = cell_width * HexagonGeneration.layers;

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
