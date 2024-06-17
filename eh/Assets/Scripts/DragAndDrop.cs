using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isSelected = false;
    private Vector3 initialPosition;
    private Vector3 offset;

    void OnMouseDown()
    {
        isSelected = true;
        initialPosition = transform.position;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        if (isSelected)
        {
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(cursorPosition.x, cursorPosition.y, transform.position.z);
        }
    }

    void OnMouseUp()
    {
        isSelected = false;
    }
}
