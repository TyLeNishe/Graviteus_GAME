using UnityEngine;

public class HexagonClickHandler : MonoBehaviour
{
    private HexagonOutline outline;
    private ConcentrationManager concentration;
    private BuildManager buildManager;
    private static HexagonOutline currentlySelected;
    private void Start()
    {
        outline = GetComponent<HexagonOutline>();
        concentration = GetComponent<ConcentrationManager>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<HexagonOutline>();
        }

        GameObject bm = GameObject.Find("Builder");
        if (bm != null)
        {
            buildManager = bm.GetComponent<BuildManager>();
        }
        else
        {
            Debug.LogError("Builder такого нет");
        }
    }

    private void Update()
    {
        if (!buildManager.isActive && currentlySelected == outline)
        {
            currentlySelected.ToggleOutline(false);
        }
    }

    private void OnMouseDown()
    {
        if (buildManager == null || !buildManager.isActive)
        {
            Debug.Log("Build manager неактивен или ненайден :()");
            return;
        }

        if (currentlySelected != null && currentlySelected != outline)
        {
            currentlySelected.ToggleOutline(false);
        }

        outline.ToggleOutline(true);
        currentlySelected = outline;
        buildManager.panelActivate = true;
        buildManager.obs = concentration.obsConcentration;
        buildManager.ign = concentration.ignConcentration;
        buildManager.ven = concentration.venConcentration;
    }

    private void OnMouseEnter()
    {
        if (buildManager == null || !buildManager.isActive) return;

        outline.ToggleOutline(true);
    }

    private void OnMouseExit()
    {
        if (currentlySelected != outline)
        {
            outline.ToggleOutline(false);
        }
    }
}