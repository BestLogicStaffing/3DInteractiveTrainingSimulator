using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Allows the player to interact using their prefab "InteractCube" on objects with "interact cube"
 * This script is attached to the player
 */

public class PanelInteraction : MonoBehaviour
{
    public CanvasController cc;
    public PlayerMovement playerMovement;
    bool currently_interacting = false;
    public bool currently_optioning = false;
    InteractableObject obj;

    private void Update()
    {
        if (currently_interacting && Input.GetKeyDown(KeyCode.Space) && !currently_optioning && (cc.panels[obj.panel_type].message_text.text == obj.messages[cc.index]))
        {   //when the player can skip to the next message
            cc.NextLine();
        }
        if (cc.index == -1) //no more text
        {
            currently_interacting = false;
            playerMovement.canMove = true;
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
