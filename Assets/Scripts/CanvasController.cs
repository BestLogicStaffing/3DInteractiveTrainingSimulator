using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Keeps control of the panels to show information to the player
 * This is not the actual panels, but it instead turns them on/off and controls the text on them
 * 
 * This video was used to help text scroll and for objects to have more than 1 message
 * https://www.youtube.com/watch?v=8oTYabhj248&ab_channel=BMo
 */

public class CanvasController : MonoBehaviour
{
    public List<PanelClass> panels;
    public GameObject interaction_notice_text, continue_notice_text;
    public float textSpeed; //how many seconds it takes to display the next character
    public int index; //to track how many messages are left in an InteractableObject's message list

    //When the player presses SPACE next to an "InteractableObject" you "Interact" with it
    public void Interact(InteractableObject obj)
    {
        if (obj.panel_type == 0) //panel with image
        {
            panels[0].panel_object.SetActive(true);
            panels[0].sprite.GetComponent<Image>().sprite = obj.sprite;
            panels[0].name_text.text = obj.name_text;
        }
        else //panel with no image
        {
            panels[1].panel_object.SetActive(true);
        }
        index = 0;
        StartCoroutine(TypeLine(obj));
    }

    public void Disinteract()
    {
        panels[0].panel_object.SetActive(false);
        panels[1].panel_object.SetActive(false);
    }

    IEnumerator TypeLine(InteractableObject obj)
    {
        panels[obj.panel_type].message_text.text = string.Empty;
        foreach (char c in obj.messages[index].ToCharArray())
        {
            panels[obj.panel_type].message_text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        //display the CONTINUE message
        if (index < obj.messages.Length - 1)
        {
            continue_notice_text.SetActive(true);
            if (obj.panel_type == 0) //move it according to which panel is showing
            {
                continue_notice_text.transform.localPosition = new Vector3(220, -214, 0);
            }
            else
            {
                continue_notice_text.transform.localPosition = new Vector3(0, -214, 0);
            }
        }
    }

    public void NextLine(InteractableObject obj)
    {
        continue_notice_text.SetActive(false); //hide the CONTINUE message
        if (index < obj.messages.Length - 1)
        {
            index++;
            panels[obj.panel_type].message_text.text = string.Empty;
            StartCoroutine(TypeLine(obj));
        }
        else
        {
            index = -1; //shows that there is no more text left
            panels[0].panel_object.SetActive(false);
            panels[1].panel_object.SetActive(false);
        }
    }
}
