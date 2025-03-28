using UnityEngine;

public class HexagonResources : MonoBehaviour
{
    public int pollution = -1, ResourcesFertility = -1;

    public void ActivateResources()
    {
        pollution = Random.Range(0, 6);
        ResourcesFertility = Random.Range(0, 6);
    }
}
