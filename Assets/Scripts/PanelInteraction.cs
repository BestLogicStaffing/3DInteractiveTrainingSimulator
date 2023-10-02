using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Allows the player to interact with the prefab "interactable cube"
 */

public class PanelInteraction : MonoBehaviour
{
    public CanvasController cc;
    public PlayerMovement playerMovement;
    bool currently_interacting = false;
    float cancel_time;

    private void Update()
    {
        if(cancel_time > 0)
        {
            cancel_time -= Time.deltaTime;
        }
        if(cancel_time <= 0 && currently_interacting && Input.GetKeyDown(KeyCode.Space))
        {
            currently_interacting = false;
            playerMovement.canMove = true;
            cc.Disinteract();
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Interactable")
        {
            cc.interaction_notice_text.SetActive(true); //show "Press SPACE to interact"
            if (Input.GetKeyDown(KeyCode.Space) && !currently_interacting)
            {
                cancel_time = 0.05f;
                currently_interacting = true;
                playerMovement.canMove = false;
                InteractableObject obj = other.GetComponent<InteractableObject>();
                cc.Interact(obj.panel_type, obj.sprite, obj.name_text, obj.message_text);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //hide "Press SPACE to interact"
        cc.interaction_notice_text.SetActive(false);
    }
}
