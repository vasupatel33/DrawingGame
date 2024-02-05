using UnityEngine;

public class ColorButtonHandler : MonoBehaviour
{
    // LineDrawer reference to set the selected color
    public LineDrawer lineDrawer;

    // Public method to set the selected color
    public void SetSelectedColor(string colorName)
    {
        // Convert color name to Color object and set it in the LineDrawer
        lineDrawer.ChangeLineColor(GetColorByName(colorName));
    }

    // Method to get Color object by name
    private Color GetColorByName(string name)
    {
        switch (name)
        {
            case "Red":
                return Color.red;
            case "Green":
                return Color.green;
            case "Blue":
                return Color.blue;
            case "Yellow":
                return Color.yellow;
            case "Black":
                return Color.black;
            default:
                return Color.black; // Default color
        }
    }
}
