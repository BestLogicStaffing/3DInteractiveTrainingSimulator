using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * NOT FINISHED
 * 
 * When player is in CleanUp level, allow the objects to be dragged and dropped
 * EVENTUALLY, destroy the trash when they are over a trash can
 * 
 * it also changes the material of an object so dont use objects with no materials for now
 */

public class DragObject : MonoBehaviour
{
    MeshRenderer mr;
    Color start_color;

    Vector3 offset;
    float mouse_z;

    bool being_held = false;
    public bool in_clean_up = false; //don't want the objects to highlight when it is not even clean up time

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        start_color = mr.material.color;
    }

    private void OnMouseDown()
    {
        if (in_clean_up)
        {
            mouse_z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            offset = gameObject.transform.position - GetMouseWorldPos();
            being_held = true;
        }
    }

    private void OnMouseUp()
    {
        being_held = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mouse_point = Input.mousePosition;
        mouse_point.z = mouse_z;
        return Camera.main.ScreenToWorldPoint(mouse_point);
    }

    private void OnMouseDrag()
    {
        if (in_clean_up)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }
    void OnMouseOver()
    {
        if (in_clean_up)
        {
            mr.material.color = new Color(1, 0, 0, 1);
        }
    }
    void OnMouseExit()
    {
        if (!being_held)
        {
            mr.material.color = start_color;
        }
    }

}
