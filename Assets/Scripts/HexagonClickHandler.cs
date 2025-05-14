using UnityEngine;

public class HexagonClickHandler : MonoBehaviour
{
    private HexagonOutline outline;
    private static HexagonOutline currentlySelected;

    private void Start()
    {
        outline = GetComponent<HexagonOutline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<HexagonOutline>();
        }
    }

    private void OnMouseDown()
    {
        if (currentlySelected != null)
        {
            currentlySelected.ToggleOutline(false);
        }
        
        outline.ToggleOutline(true);
        currentlySelected = outline;
    }
}