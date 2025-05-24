using UnityEngine;

public class CameraHovering : MonoBehaviour
{
    private Vector3 moveVector, scrollVector, tiltVector;
    private float moveMult = 1f, CameraTilt;
    private float moveSpeed;
    private float scrollSpeed = 450f;
    private static bool TerminalState = false;

    private Vector3 targetPosition;  // Целевая позиция для плавного перемещения
    private Vector3 targetRotation;  // Целевой угол наклона камеры
    private float smoothTime = 0.6f; // Время для сглаживания
    private Vector3 velocity = Vector3.zero;  // Для расчета скорости сглаживания

    public GameObject Terminal;
    void Start()
    {
        targetPosition = transform.position;  // Изначальная позиция
        targetRotation = transform.eulerAngles;  // Изначальный угол наклона
    }

    void Update()
    {
        TerminalState = Terminal.activeSelf;
        Vector3 CamPos = transform.position;

        bool Key_W = Input.GetKey(KeyCode.W);
        bool Key_A = Input.GetKey(KeyCode.A);
        bool Key_S = Input.GetKey(KeyCode.S);
        bool Key_D = Input.GetKey(KeyCode.D);

        int east_scroll_enabled, west_scroll_enabled;
        int up_scroll_enabled, down_scroll_enabled;
        int north_scroll_enabled, south_scroll_enabled;

        // Проверка границ мира
        if (CamPos.x >= WorldBorders.East_Border) { east_scroll_enabled = 0; } else { east_scroll_enabled = 1; }
        if (CamPos.x <= WorldBorders.West_Border) { west_scroll_enabled = 0; } else { west_scroll_enabled = 1; }

        if (CamPos.y >= WorldBorders.Upper_Border) { up_scroll_enabled = 0; }
        else { up_scroll_enabled = 1; }
        if (CamPos.y <= WorldBorders.Bottom_Border) { down_scroll_enabled = 0; }
        else { down_scroll_enabled = 1; }

        if (CamPos.z >= WorldBorders.North_Border) { north_scroll_enabled = 0; } else { north_scroll_enabled = 1; }
        if (CamPos.z <= WorldBorders.South_Border) { south_scroll_enabled = 0; } else { south_scroll_enabled = 1; }

        // Формулы скорости и наклона камеры
        moveSpeed = 1.5f * moveMult * WorldBorders.CamVelDist_Mult;
        CameraTilt = (Mathf.Pow(CamPos.y, 1.9f) / 17) + 30;

        // SHIFT
        if (Input.GetKey(KeyCode.LeftShift)) { moveMult = 2; } else { moveMult = 1; }
        if (HexagonSaveLoad.menuOff0rOn == false && !TerminalState)
        {
            // WASD движение
            if (Key_W) { DirectionChosen.hov_z = 1f * north_scroll_enabled; smoothTime = 0.2f; }
            if (Key_S) { DirectionChosen.hov_z = -1f * south_scroll_enabled; smoothTime = 0.2f; }
            if (Key_D) { DirectionChosen.hov_x = 1f * east_scroll_enabled; smoothTime = 0.2f; }
            if (Key_A) { DirectionChosen.hov_x = -1f * west_scroll_enabled; smoothTime = 0.2f; }

            // Остановка движения
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) { DirectionChosen.hov_z = 0f; smoothTime = 0.6f; }
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) { DirectionChosen.hov_x = 0f; smoothTime = 0.6f; }

            // Скроллинг
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && up_scroll_enabled == 1) { scrollVector = new Vector3(0, Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed * up_scroll_enabled, -CameraTilt / 100 * up_scroll_enabled); }
            else if (Input.GetAxis("Mouse ScrollWheel") == 0 && up_scroll_enabled == 1) { scrollVector = new Vector3(0, 0, 0); }

            if (Input.GetAxis("Mouse ScrollWheel") > 0 && down_scroll_enabled == 1) { scrollVector = new Vector3(0, Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed * down_scroll_enabled, CameraTilt / 100 * down_scroll_enabled); }
            else if (Input.GetAxis("Mouse ScrollWheel") == 0 && down_scroll_enabled == 1) { scrollVector = new Vector3(0, 0, 0); }

            // Перемещение камеры
            moveVector = new Vector3(moveSpeed * DirectionChosen.hov_x, (-(up_scroll_enabled - 1) + (down_scroll_enabled - 1)) * -scrollSpeed / 12, moveSpeed * DirectionChosen.hov_z);

            if (smoothTime == 0.2f) { scrollSpeed = 600f; }
            else { scrollSpeed = 1200f; }

            // Целевая позиция
            targetPosition = transform.position + scrollVector + moveVector;

            // Плавное перемещение
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // Плавный наклон камеры
            targetRotation = new Vector3(CameraTilt, 0, 0);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetRotation, Time.deltaTime * 10f);
        }
    }
}
