using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Fall 2023:
 * Keeps control of the panels to show information to the player
 * This is not the actual panels, but it instead turns them on/off and controls the text on them
 * 
 * This video was used to help text scroll and for objects to have more than 1 message
 * https://www.youtube.com/watch?v=8oTYabhj248&ab_channel=BMo
 * 
 * Really try to understand this because CanvasController is an important script as it is used everywhere
 */

public class CanvasController : MonoBehaviour
{
    public List<PanelClass> panels;
    public GameObject interaction_notice_text, continue_notice_text;
    public GameObject[] option_boxes;
    public TMP_Text[] option_boxes_text;
    public float textSpeed; //how many seconds it takes to display the next character
    public int index; //to track how many messages are left in an InteractableObject's message list
    public int option_chosen, option_index, option_message_index; //when player clicks on option button it saves which option was chosen
    InteractableObject obj;

    public PanelInteraction panelInteraction;
    public CheckListScript checkList;

    //When the player presses SPACE next to an "InteractableObject" you "Interact" with it
    public void Interact(InteractableObject obj)
    {
        this.obj = obj;
        if (obj.panel_type == 0) //panel with image and text
        {
            panels[0].panel_object.SetActive(true);
            panels[0].sprite.GetComponent<Image>().sprite = obj.sprites[0];
            panels[0].name_text.text = obj.name_text;
        }
        else if (obj.panel_type == 1) //panel with text
        {
            panels[1].panel_object.SetActive(true);
        }
        else if(obj.panel_type == 2) //panel with image (also the only panel that can change its sprite)
        {
            panels[2].panel_object.SetActive(true);
            panels[2].sprite.GetComponent<Image>().sprite = obj.sprites[obj.sprite_index];
            if (obj.has_multiple_sprites) //maybe the object does not need to "change sprites" EX: signing contract
            {
                panels[2].message_text.text = obj.messages[0];
                panels[2].button.SetActive(true);
            }
            else
            {
                panels[2].button.SetActive(false);
            }
            if (obj.sprite_index == obj.sprites.Length - 1) //hide the button because all sprites have been used
            {
                panels[2].button.SetActive(false);
            }
            Cursor.lockState = CursorLockMode.None; //allow player to move cursor
            Cursor.visible = true;
        }
        else{Debug.Log("panel type is not 0-2");}   //debug

        index = 0; option_index = 0; option_message_index = -1;
        if(obj.panel_type != 2)
        {
            StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine()
    {
        //show message
        panels[obj.panel_type].message_text.text = string.Empty;
        foreach (char c in obj.messages[index].ToCharArray())
        {
            panels[obj.panel_type].message_text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        if (index < obj.messages.Length - 1)
        {
            if(!obj.has_options || (obj.has_options && option_index <= obj.when_show_options.Length - 1 && obj.when_show_options[option_index] != index))
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
        panelInteraction.currently_optioning = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (obj.has_options) //if the player has to select an option
        {
            option_chosen = -999;
            //show options
            int i = -1;
            if((option_index < obj.when_show_options.Length) && (obj.when_show_options[option_index] == index)) //when it reaches a point to show an option
            {
                Cursor.lockState = CursorLockMode.None; //allow player to move cursor
                Cursor.visible = true;
                panelInteraction.currently_optioning = true;
                option_index++;
                do
                {
                    i++;
                    option_message_index++;
                    option_boxes[i].SetActive(true);
                    foreach(GameObject box in option_boxes)
                    {
                        Vector3 newPos = box.transform.localPosition; //move boxes based on how many options are available
                        newPos.x -= 125;
                        box.transform.localPosition = newPos;
                    }
                    option_boxes_text[i].text = obj.option_messages[option_message_index];
                } while (option_message_index < obj.option_messages.Length - 1 && obj.option_messages[option_message_index + 1] != ""); //options are split up by spaces and stop showing when there are no more options left
                option_message_index++;
            }
        }
    }

    public void NextLine() //resets all text to work for the next line
    {
        continue_notice_text.SetActive(false); //hide the CONTINUE message
        foreach (GameObject box in option_boxes)
        {
            //move boxes back
            Vector3 newPos = box.transform.localPosition;
            newPos.x += 375;
            box.transform.localPosition = newPos;
            box.SetActive(false);
        }
        if (index < obj.messages.Length - 1)
        {
            index++;
            panels[obj.panel_type].message_text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            ClosePanels();
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ChooseOption(int i)
    {
        option_chosen = i;
        NextLine();
    }

    public void ChangePanel2Sprite()
    {
        //go to next sprite
        obj.sprite_index++;
        panels[2].sprite.GetComponent<Image>().sprite = obj.sprites[obj.sprite_index];
        if (obj.sprite_index == obj.sprites.Length - 1) //hide the button because there are no more sprites to show
        {
            panels[2].button.SetActive(false);
        }
    }

    public void ClosePanels()
    {
        if (obj.item != "")
        {
            Debug.Log("give item: " + obj.item);
            checkList.AddItem(obj.item);
        }
        panelInteraction.currently_optioning = false;
        index = -1; //shows that there is no more text left
        panels[0].panel_object.SetActive(false);
        panels[1].panel_object.SetActive(false);
        panels[2].panel_object.SetActive(false);
    }

}
