using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject linePrefab; // Prefab for the line object
    private GameObject currentLine; // Reference to the current line object
    private LineRenderer lineRenderer; // LineRenderer component of the current line object
    private Vector2 previousTouchPosition; // Previous touch position
    private Color selectedColor = Color.red; // Default color

    [SerializeField] GameObject parentObject;

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
        currentLine = Instantiate(linePrefab, touchPosition, Quaternion.identity, parentObject.transform);
        lineRenderer = currentLine.GetComponent<LineRenderer>();

        // Set initial line position
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(touchPosition));

        // Set line color
        UpdateLineColor(selectedColor);

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

        // Update previous touch position
        previousTouchPosition = touchPosition;
    }




    public void SetSelectedColor(Color color)
    {
        selectedColor = color;
        UpdateLineColor(selectedColor);
    }
    void UpdateLineColor(Color color)
    {
        // Ensure that LineRenderer component is assigned
        if (lineRenderer != null)
        {
            // Set the new color
            lineRenderer.material.color = color;
        }
        else
        {
            Debug.LogWarning("LineRenderer component is not assigned.");
        }
    }

    public void ChangeLineColor(Color color)
    {
        // Ensure that LineRenderer component is assigned
        if (lineRenderer != null)
        {
            // Set the new color
            selectedColor = color;
            lineRenderer.startColor = selectedColor;
            lineRenderer.endColor = selectedColor;
        }
        else
        {
            Debug.LogWarning("LineRenderer component is not assigned.");
        }
    }
    
    public void ClearScreen()
    {

        // Check if the parent object exists
        if (parentObject != null)
        {
            // Get all child objects
            Transform[] children = parentObject.GetComponentsInChildren<Transform>();

            // Iterate through each child and destroy them
            foreach (Transform child in children)
            {
                if (child != parentObject.transform) // Avoid destroying the parent itself
                {
                    Destroy(child.gameObject);
                }
            }
        }
        else
        {
            Debug.LogWarning("Parent object of lines not found.");
        }
    }
}
