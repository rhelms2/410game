using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshWireframeScalable : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private Mesh originalMesh;

    // Define min and max thickness for scaling
    public float minThickness = 0f;
    public float maxThickness = 10f;
    public float minDistance = 1f;
    public float maxDistance = 50f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        originalMesh = meshFilter.sharedMesh;
    }

    private void Update()
    {
        if (mainCamera == null)
            return;

        // Calculate distance between camera and object
        float distance = Vector3.Distance(mainCamera.transform.position, transform.position);

        // Map distance to thickness
        float thickness = Map(distance, minDistance, maxDistance, minThickness, maxThickness);

        // Apply thickness to wireframe
        UpdateWireframeThickness(thickness);
    }

    private void UpdateWireframeThickness(float thickness)
    {
        meshRenderer.materials[0].SetFloat("_Wireframe_Thickness", thickness);
    }

    // Utility function to map a value from one range to another
    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }
}
