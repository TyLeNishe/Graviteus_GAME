using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class HexagonOutline : MonoBehaviour
{
    [SerializeField] private float outlineWidth = 0.03f;

    private LineRenderer lineRenderer;
    private MeshFilter meshFilter;
    private HexagonLandscape landscape;
    private Color defaultColor = new Color32(126, 241, 0, 255);
    private Color landscapeColor = Color.red;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        landscape = GetComponent<HexagonLandscape>();
        InitializeOutline();
    }

    private void InitializeOutline()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.loop = true;
        lineRenderer.startWidth = outlineWidth;
        lineRenderer.endWidth = outlineWidth;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.useWorldSpace = false;
        lineRenderer.enabled = false;

        UpdateOutlineColor();
        UpdateOutlineShape();
    }

    private void UpdateOutlineColor()
    {
        if (landscape.mountain == true || landscape.rift == true)
        {
            lineRenderer.material.color = landscape.IsDefault() ? defaultColor : landscapeColor;
        }
        else
        {
            lineRenderer.material.color = defaultColor;
        }
    }

    private void UpdateOutlineShape()
    {
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            Vector3[] vertices = meshFilter.sharedMesh.vertices;
            lineRenderer.positionCount = vertices.Length;
            lineRenderer.SetPositions(vertices);
        }
    }

    public void ToggleOutline(bool state)
    {
        if (lineRenderer == null)
            InitializeOutline();

        lineRenderer.enabled = state;
        if (state) UpdateOutlineColor();
    }
}