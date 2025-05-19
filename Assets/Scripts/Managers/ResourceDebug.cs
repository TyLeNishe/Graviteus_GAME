using System.Collections.Generic;
using UnityEngine;

public class ResourceDebug : MonoBehaviour
{
    public int inc = 1, mult = 1;

    public void IncModePositive() { inc = 1; }
    public void IncModeNegative() { inc = -1; }

    public void MultModeX1() { mult = 1; }
    public void MultModeX5() { mult = 5; }
    public void MultModeX10() { mult = 10; }
    public void MultModeX100() { mult = 100; }

    public void ObsChange() { ResourceManager.resources["obs"] += inc * mult; }
    public void IgnChange() { ResourceManager.resources["ign"] += inc * mult; }
    public void VenChange() { ResourceManager.resources["ven"] += inc * mult; }


}

