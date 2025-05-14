using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class HexagonOutline : MonoBehaviour
{
    [SerializeField] private Color customColor = Color.red;
    [SerializeField] private bool useRandomColor = true;
    [SerializeField] private float outlineWidth = 0.06f;

    private LineRenderer lineRenderer;
    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        InitializeOutline();
    }

    private void InitializeOutline()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.loop = true;
        lineRenderer.startWidth = outlineWidth;
        lineRenderer.endWidth = outlineWidth;

        Color outlineColor = useRandomColor ? GetRandomColor() : customColor;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = outlineColor };

        lineRenderer.useWorldSpace = false;
        lineRenderer.enabled = false;

        UpdateOutlineShape();
    }

    private Color GetRandomColor()
    {
        return Random.Range(0, 3) switch
        {
            0 => new Color(0.2f, 0.8f, 0.2f), // Тёмно-зелёный
            1 => new Color(1f, 0.8f, 0f),     // Золотисто-жёлтый
            2 => new Color(0.8f, 0.1f, 0.1f), // Тёмно-красный
            _ => Color.white
        };
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
    }
}