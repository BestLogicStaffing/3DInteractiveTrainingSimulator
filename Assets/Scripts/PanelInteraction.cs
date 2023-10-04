using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Allows the player to interact using their prefab "InteractCube" on objects with "interact cube"
 */

public class PanelInteraction : MonoBehaviour
{
    public CanvasController cc;
    public PlayerMovement playerMovement;
    bool currently_interacting = false;

    InteractableObject obj;

    private void Update()
    {
        if(currently_interacting && Input.GetKeyDown(KeyCode.Space) && (cc.panels[obj.panel_type].message_text.text == obj.messages[cc.index]) )
        {
            cc.NextLine(obj);
            if(cc.index == -1) //no more text
            {
                currently_interacting = false;
                playerMovement.canMove = true;
            }
        }        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Interactable")
        {
            cc.interaction_notice_text.SetActive(true); //show "Press SPACE to interact"
            if (Input.GetKeyDown(KeyCode.Space) && !currently_interacting)
            {
                currently_interacting = true;
                playerMovement.canMove = false;

                //display the panel text
                obj = other.GetComponent<InteractableObject>();
                cc.Interact(obj);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //hide "Press SPACE to interact"
        cc.interaction_notice_text.SetActive(false);
    }
}
