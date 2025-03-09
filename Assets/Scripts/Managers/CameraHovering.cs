using UnityEngine;

public class CameraHovering : MonoBehaviour
{
    public Vector3 moveVector;
    public float moveMult = 1f;
    public float moveSpeed;
    public GameObject TerminalObj;

    void Update()
    {
        moveSpeed = 0.01f * moveMult;
        if (Input.GetKey(KeyCode.LeftShift)) { moveMult = 2; } else { moveMult = 1; }

        if (Input.GetKey(KeyCode.W)) { if (DirectionChosen.hov_y < DirectionChosen.hov_y_cap_up) { DirectionChosen.hov_y += 1f; } }
        if (Input.GetKey(KeyCode.S)) { if (DirectionChosen.hov_y > DirectionChosen.hov_y_cap_down) { DirectionChosen.hov_y -= 1f; } }
        if (Input.GetKey(KeyCode.D)) { if (DirectionChosen.hov_x < DirectionChosen.hov_x_cap_up) { DirectionChosen.hov_x += 1f; } }
        if (Input.GetKey(KeyCode.A)) { if (DirectionChosen.hov_x > DirectionChosen.hov_x_cap_down) { DirectionChosen.hov_x -= 1f; } }

        if (Input.GetKeyUp(KeyCode.W)) { DirectionChosen.hov_y = 0f; }
        if (Input.GetKeyUp(KeyCode.S)) { DirectionChosen.hov_y = 0f; }
        if (Input.GetKeyUp(KeyCode.D)) { DirectionChosen.hov_x = 0f; }
        if (Input.GetKeyUp(KeyCode.A)) { DirectionChosen.hov_x = 0f; }

        moveVector = new Vector3(moveSpeed * DirectionChosen.hov_x, 0, moveSpeed * DirectionChosen.hov_y);
        transform.position += moveVector;
    }
}
