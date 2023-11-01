using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    Vector3 offset;
    float mouse_z;

    private void OnMouseDown()
    {
        mouse_z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mouse_point = Input.mousePosition;
        mouse_point.z = mouse_z;
        return Camera.main.ScreenToWorldPoint(mouse_point);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }
}
