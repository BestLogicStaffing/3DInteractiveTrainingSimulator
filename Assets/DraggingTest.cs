using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingTest : MonoBehaviour
{
    void Start()
    {
        foreach (GameObject trash in GameObject.FindGameObjectsWithTag("Trash"))
        {
            trash.GetComponent<DragObject>().in_clean_up = true;
        }
    }

}
