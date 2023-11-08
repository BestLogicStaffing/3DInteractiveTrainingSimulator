using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Just used in DragDropTest scene so that the objects can be moved
 */

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
