using UnityEngine;

public class CameraHovering : MonoBehaviour
{
    public Vector3 moveVector, scrollVector, tiltVector;
    public float moveMult = 1f, CameraTilt;
    public float moveSpeed, scrollSpeed = 1f;

    void Update()
    {
        Vector3 CamPos = transform.position;

        bool Key_W = Input.GetKey(KeyCode.W);
        bool Key_A = Input.GetKey(KeyCode.A);
        bool Key_S = Input.GetKey(KeyCode.S);
        bool Key_D = Input.GetKey(KeyCode.D);

        int east_scroll_enabled, west_scroll_enabled;
        int up_scroll_enabled, down_scroll_enabled;
        int north_scroll_enabled, south_scroll_enabled;

        //ÑÊÐÈÏÒ ÏÐÎÂÅÐÊÈ ÃÐÀÍÈÖ ÌÈÐÀ
        if (CamPos.x >= WorldBorders.East_Border) { east_scroll_enabled = 0; } else { east_scroll_enabled = 1; }
        if (CamPos.x <= WorldBorders.West_Border) { west_scroll_enabled = 0; } else { west_scroll_enabled = 1; }

        if (CamPos.y >= WorldBorders.Upper_Border) { up_scroll_enabled = 0; } else { up_scroll_enabled = 1; }
        if (CamPos.y <= WorldBorders.Bottom_Border) { down_scroll_enabled = 0; } else { down_scroll_enabled = 1; }

        if (CamPos.z >= WorldBorders.North_Border) { north_scroll_enabled = 0; } else { north_scroll_enabled = 1; }
        if (CamPos.z <= WorldBorders.South_Border) { south_scroll_enabled = 0; } else { south_scroll_enabled = 1; }

        //ÔÎÐÌÓËÛ ÑÊÎÐÎÑÒÈ È ÍÀÊËÎÍÀ ÊÀÌÅÐÛ
        moveSpeed = 0.01f * moveMult * WorldBorders.CamVelDist_Mult;
        CameraTilt = (Mathf.Pow(CamPos.y, 1.9f) / 5);
        scrollSpeed = 10f;

        //SHIFT
        if (Input.GetKey(KeyCode.LeftShift)) { moveMult = 2; } else { moveMult = 1; }


        //WASD ÄÂÈÆÅÍÈÅ
        if (Key_W) { DirectionChosen.hov_z = 1f * north_scroll_enabled; }

        if (Key_S) { DirectionChosen.hov_z = -1f * south_scroll_enabled; }

        if (Key_D) { DirectionChosen.hov_x = 1f * east_scroll_enabled; }

        if (Key_A) { DirectionChosen.hov_x = -1f * west_scroll_enabled; }



        // ÎÑÒÀÍÎÂÊÀ ÄÂÈÆÅÍÈß
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) { DirectionChosen.hov_z = 0f; }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) { DirectionChosen.hov_x = 0f; }

        //ÑÊÐÎËË
        if (Input.GetAxis("Mouse ScrollWheel") < 0) { scrollVector = new Vector3(0, Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed * up_scroll_enabled, -CameraTilt / 100 * up_scroll_enabled); }
        else if (Input.GetAxis("Mouse ScrollWheel") == 0) { scrollVector = new Vector3(0, 0, 0); }

        if (Input.GetAxis("Mouse ScrollWheel") > 0) { scrollVector = new Vector3(0, Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed * down_scroll_enabled, CameraTilt / 100 * down_scroll_enabled); }
        else if (Input.GetAxis("Mouse ScrollWheel") == 0) { scrollVector = new Vector3(0, 0, 0); }

        //ÏÅÐÅÌÅÙÅÍÈÅ ÊÀÌÅÐÛ
        moveVector = new Vector3(moveSpeed * DirectionChosen.hov_x, 0, moveSpeed * DirectionChosen.hov_z);
        tiltVector = new Vector3(CameraTilt, 0, 0);


        if (!Input.anyKey) // ýòî ïðîâåðêà íà êëàâèøè, ìîë íàæàòà õîòÿ-áû îäíà êëàâèøà
        {
            Vector3 mousePosition = Input.mousePosition;

            if (mousePosition[0] >= Screen.width * 0.9f) { DirectionChosen.hov_x = 1 * east_scroll_enabled; }
            else if (mousePosition[0] <= Screen.width * 0.1f) { DirectionChosen.hov_x = -1 * west_scroll_enabled; }
            else { DirectionChosen.hov_x = 0; }

            if (mousePosition[1] >= Screen.height * 0.95f) { DirectionChosen.hov_z = 1 * north_scroll_enabled; }
            else if (mousePosition[1] <= Screen.height * 0.1f) { DirectionChosen.hov_z = -1 * south_scroll_enabled; }
            else { DirectionChosen.hov_z = 0; }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) { DirectionChosen.hov_z = 0f; }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) { DirectionChosen.hov_x = 0f; }

        transform.position += scrollVector + moveVector;
        transform.eulerAngles = tiltVector;
    }
}