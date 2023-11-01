using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Fall 2023:
 * Allows the player to interact using their prefab "InteractCube" on objects with "interact cube"
 * This script is attached to the player
 */

public class PanelInteraction : MonoBehaviour
{
    public CanvasController cc;
    [SerializeField]
    CheckListScript checkList;
    public PlayerMovement playerMovement;
    public bool currently_interacting = false;
    public bool currently_optioning = false;
    InteractableObject obj;

    bool displaying_panel_2; //just used when displaying panel 2, it does not use text

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!displaying_panel_2) //panel 2 has no messages so don't even try doing NextLine()
        {
            if (currently_interacting && !currently_optioning && Input.GetKeyDown(KeyCode.Space) && (cc.panels[obj.panel_type].message_text.text == obj.messages[cc.index]))
            {   //determines when the player can skip to the next message
                cc.NextLine();
            }
        }
        
        if (cc.index == -1) //no more text
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            currently_interacting = false;
            playerMovement.canMove = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Interactable")) //interacting with an "InteractableObject"
        {
            //prevent the player from completing stuff out of order or without the CheckList
            if ((checkList.checkListAppeared && other.GetComponent<InteractableObject>().item_slot == checkList.total_items) ||
                other.GetComponent<InteractableObject>().item_slot == -99 ||
                other.GetComponent<InteractableObject>().item_slot == -1)
            {
                cc.interaction_notice_text.SetActive(true); //show "Press SPACE to interact"
                if (Input.GetKeyDown(KeyCode.Space) && !currently_interacting)
                {
                    currently_interacting = true;
                    playerMovement.canMove = false;
                    //display the panel text
                    obj = other.GetComponent<InteractableObject>();
                    cc.Interact(obj);
                    displaying_panel_2 = obj.panel_type == 2;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //hide "Press SPACE to interact"
        if(other.CompareTag("Interactable"))
        {
            cc.interaction_notice_text.SetActive(false);
        }
    }
}
