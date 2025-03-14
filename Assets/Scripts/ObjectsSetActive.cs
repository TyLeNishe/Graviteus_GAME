using UnityEngine;

public class ObjectsSetActive : MonoBehaviour
{
    public GameObject Terminal;
    public GameObject Initialization;
    public GameObject Head;
    public GameObject Main;
    public GameObject Furnace;
    public GameObject Printer;
    public GameObject Synthesis;
    void OnEnable()
    {
        Terminal.gameObject.SetActive(true);
        Initialization.gameObject.SetActive(true);
        Head.gameObject.SetActive(false);
        Main.gameObject.SetActive(false);
        Furnace.gameObject.SetActive(false);
        Printer.gameObject.SetActive(false);
        Synthesis.gameObject.SetActive(false);
    }
}
