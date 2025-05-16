using UnityEngine;

public class ConcentrationManager : MonoBehaviour
{
    public int obsConcentration;
    public int ignConcentration;
    public int venConcentration;
    private void Awake()
    {
        obsConcentration = GetRandomConcentration();
        ignConcentration = GetRandomConcentration();
        venConcentration = GetRandomConcentration();
    }
    private int GetRandomConcentration()
    {
        return Random.Range(0, 100);
    }
}
