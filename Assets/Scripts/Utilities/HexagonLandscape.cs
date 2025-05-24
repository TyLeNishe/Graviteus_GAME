using UnityEngine;

public class HexagonLandscape : MonoBehaviour
{
    public bool mountain = false, rift = false, geyser = false, meteorite = false, fireoilpool = false, factory = false ;

    public bool IsDefault()
    {
        return !mountain && !rift && !geyser && !meteorite && !fireoilpool;
    }
    public void ActivateMountain()
    {
        mountain = true;
    }
    public void ActivateRift()
    {
        rift = true;
    }
    public void ActivateFactory()
    {
        factory = true;
    }
    public void ActivateMeteorite()
    {
        meteorite = true;
    }
    public void ActivateGeyser()
    {
        geyser = true;
    }
    public void ActivateFireoilPool()
    {
        fireoilpool = true;
    }
}