using System.Collections.Generic;
using UnityEngine;

public class ResourceDebug : MonoBehaviour
{
    public static Dictionary<string, float> storage =
    new Dictionary<string, float>();

    void Start()
    {
        storage.Add("obs", 0);
        storage.Add("ign", 0);
        storage.Add("ven", 0);
        storage.Add("val", 0);
        storage.Add("inst", 0);
        storage.Add("pug", 0);
    }

    public int inc = 1, mult = 1;

    public void IncModePositive() { inc = 1; }
    public void IncModeNegative() { inc = -1; }

    public void MultModeX1() { mult = 1; }
    public void MultModeX5() { mult = 5; }
    public void MultModeX10() { mult = 10; }
    public void MultModeX100() { mult = 100; }

    public void ObsChange() { storage["obs"] += inc * mult; }
    public void IgnChange() { storage["ign"] += inc * mult; }
    public void VenChange() { storage["ven"] += inc * mult; }


}

