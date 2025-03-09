using UnityEngine;

public class HexagonLandscape : MonoBehaviour
{
    public bool mountain = false, rift = false;

    public void ActivateMountain()
    {
        mountain = true;
    }

    public void ActivateRift()
    {
        rift = true;
    }
}