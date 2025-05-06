using UnityEngine;

public class ResourceDebug : MonoBehaviour
{
    public static int obs = 0, ign = 1, ven = 2;
    public int inc = 1, mult = 1;

    public void IncModePositive(){ inc = 1; }
    public void IncModeNegative() { inc = -1; }

    public void MultModeX1() { mult = 1; }
    public void MultModeX5() { mult = 5; }
    public void MultModeX10() { mult = 10; }
    public void MultModeX100() { mult = 100; }

    public void ObsChange() { obs += inc * mult; }
    public void IgnChange() { ign += inc * mult; }
    public void VenChange() { ven += inc * mult; }


}   

