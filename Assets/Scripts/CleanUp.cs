using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A script that moves the camera so the player can clean up a table for the third task
 * This script is attached to the Canvas
 */

public class CleanUp : MonoBehaviour
{
    [SerializeField]
    GameObject player_camera, clean_up_camera;

    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    GameObject checkList_object;

    GameObject selected_object;

    private void Start()
    {
        player_camera.SetActive(true);
        clean_up_camera.SetActive(false);
    }

    public void BeginCleanUp()
    {
        gameObject.GetComponent<CanvasController>().index = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player_camera.SetActive(false); player_camera.gameObject.tag = "Untagged";
        clean_up_camera.SetActive(true); clean_up_camera.gameObject.tag = "MainCamera";
        checkList_object.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void FinishCleanUp()
    {
        gameObject.GetComponent<CanvasController>().index = -1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player_camera.SetActive(true);
        clean_up_camera.SetActive(false);
        checkList_object.GetComponent<CanvasGroup>().alpha = 1;
    }

    //https://www.youtube.com/watch?v=uNCCS6DjebA&ab_channel=AIA
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selected_object == null)
            {
                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Drag"))
                    {
                        return;
                    }
                    selected_object = hit.collider.gameObject;
                }
            }
            else
            {

            }
        }
        if(selected_object != null)
        {
            Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selected_object.transform.position).z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
            selected_object.transform.position = new Vector3(worldPos.x, 0.25f, worldPos.z);
        }
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;

    }
}
