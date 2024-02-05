using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject linePrefab; // Prefab for the line object
    private GameObject currentLine; // Reference to the current line object
    private LineRenderer lineRenderer; // LineRenderer component of the current line object
    private Vector2 previousTouchPosition; // Previous touch position
    private Color selectedColor = Color.red; // Default color

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("down");
            CreateNewLine(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Debug.Log("..");
            UpdateLine(Input.mousePosition);
        }
    }

    void CreateNewLine(Vector2 touchPosition)
    {
        // Instantiate a new line object
        currentLine = Instantiate(linePrefab, touchPosition, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();

        // Copy LineRenderer component from prefab
        LineRenderer prefabLineRenderer = linePrefab.GetComponent<LineRenderer>();
        if (prefabLineRenderer != null)
        {
            lineRenderer.material = prefabLineRenderer.material; // Copy material
            lineRenderer.widthCurve = prefabLineRenderer.widthCurve; // Copy width curve
            lineRenderer.widthMultiplier = prefabLineRenderer.widthMultiplier; // Copy width multiplier
        }
        else
        {
            Debug.LogWarning("Line prefab is missing LineRenderer component.");
        }

        // Set initial line position
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(touchPosition));

        // Set line color
        if (selectedColor != null)
        {
            lineRenderer.startColor = selectedColor;
            lineRenderer.endColor = selectedColor;
        }
        else
        {
            lineRenderer.startColor = Color.black; // Default to black if selectedColor is not set
            lineRenderer.endColor = Color.black; // Default to black if selectedColor is not set
        }

        // Set the previous touch position
        previousTouchPosition = touchPosition;
    }



    void UpdateLine(Vector2 touchPosition)
    {
        // Calculate world position of touch
        Vector2 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

        // Add a new point to the line
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, touchWorldPosition);

        // Update line color (ensure it reflects any color changes)
        lineRenderer.startColor = selectedColor;
        lineRenderer.endColor = selectedColor;

        // Update previous touch position
        previousTouchPosition = touchPosition;
    }


    public void SetSelectedColor(Color color)
    {
        selectedColor = color;
        Debug.Log("Color name = "+selectedColor);
        Debug.Log("Color = " + color);

        if (lineRenderer != null)
        {
            lineRenderer.startColor = selectedColor;
            lineRenderer.endColor = selectedColor;
            Debug.Log("Line color = "+lineRenderer.startColor);
        }
    }

    public void ChangeLineColor(Color color)
    {
        // Ensure that LineRenderer component is assigned
        if (lineRenderer != null)
        {
            // Get the material assigned to the LineRenderer
            Material material = lineRenderer.material;

            // Set the new color
            material.color = color;
        }
        else
        {
            Debug.LogWarning("LineRenderer component is not assigned.");
        }
    }

}
