using UnityEngine;

public class CameraHovering : MonoBehaviour
{
    public Vector3 moveVector;
    public float moveMult = 1f;
    public float moveSpeed;

    void Update()
    {
        moveSpeed = 0.01f * moveMult;

        if (Input.GetKey(KeyCode.LeftShift)) { moveMult = 2; } else { moveMult = 1; }

        if (Input.GetKey(KeyCode.W) && DirectionChosen.hov_y < DirectionChosen.hov_y_cap_up){DirectionChosen.hov_y += 1f;}
        if (Input.GetKey(KeyCode.S) && DirectionChosen.hov_y > DirectionChosen.hov_y_cap_down){DirectionChosen.hov_y -= 1f;}
        if (Input.GetKey(KeyCode.D) && DirectionChosen.hov_x < DirectionChosen.hov_x_cap_up){DirectionChosen.hov_x += 1f;}
        if (Input.GetKey(KeyCode.A) && DirectionChosen.hov_x > DirectionChosen.hov_x_cap_down){DirectionChosen.hov_x -= 1f;}

        if (!Input.anyKey) // это проверка на клавиши, мол нажата хотя-бы одна клавиша
        {
            Vector3 mousePosition = Input.mousePosition;

            if (mousePosition[0] >= Screen.width * 0.8f) { DirectionChosen.hov_x = 1; }
            else if (mousePosition[0] <= Screen.width * 0.2f) { DirectionChosen.hov_x = -1; }
            else { DirectionChosen.hov_x = 0; }

            if (mousePosition[1] >= Screen.height * 0.8f) { DirectionChosen.hov_y = 1; }
            else if (mousePosition[1] <= Screen.height * 0.2f) { DirectionChosen.hov_y = -1; }
            else { DirectionChosen.hov_y = 0; }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) { DirectionChosen.hov_y = 0f; }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) { DirectionChosen.hov_x = 0f; }
        moveVector = new Vector3(moveSpeed * DirectionChosen.hov_x, 0, moveSpeed * DirectionChosen.hov_y);
        transform.position += moveVector;
    }
}