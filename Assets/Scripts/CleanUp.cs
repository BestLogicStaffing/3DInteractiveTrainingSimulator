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
        checkList_object.SetActive(false); //since some trash are under CANVAS objects so you should be able to click under them
        foreach(GameObject trash in GameObject.FindGameObjectsWithTag("Trash"))
        {
            trash.GetComponent<DragObject>().in_clean_up = true;
        }
    }

    public void FinishCleanUp()
    {
        gameObject.GetComponent<CanvasController>().index = -1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player_camera.SetActive(true); player_camera.gameObject.tag = "MainCamera";
        clean_up_camera.SetActive(false); clean_up_camera.gameObject.tag = "Untagged";
        checkList_object.SetActive(true);
    }
}
