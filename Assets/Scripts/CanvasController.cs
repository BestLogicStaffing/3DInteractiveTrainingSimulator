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
 * Really try to understand this because CanvasController is an important script that is used everywhere
 * This script is attached to the Canvas
 */

public class CanvasController : MonoBehaviour
{
    public List<PanelClass> panels;
    [SerializeField] GameObject[] option_boxes;
    [SerializeField] TMP_Text[] option_boxes_text;
    [SerializeField] GameObject[] option_positions;
    [SerializeField] GameObject answer; [SerializeField] TMP_Text answer_text; [SerializeField] Sprite correct, wrong;
    float fadeTime = 1.5f; //how many seconds it takes to fade in/out the answer image
    float textSpeed = 0.03f; //how many seconds it takes to display the next character
    public int index; //track how many messages are left in an InteractableObject's message list (index = -1 means no more messages)
    [SerializeField] int option_index, option_message_index; //option_index = which options is it on, option_message_index = which message to display on the options
    public GameObject interaction_notice_text, continue_notice_text;
    InteractableObject obj;
    public PanelInteraction panelInteraction;
    public CheckListScript checkList;

    //When the player presses SPACE next to an "InteractableObject" you "Interact" with it
    public void Interact(InteractableObject obj) //does different things based on which panel to show
    {
        this.obj = obj;
        if (obj.panel_type == 0) //panel with image and text
        {
            panels[0].panel_object.SetActive(true);
            panels[0].sprite.GetComponent<Image>().sprite = obj.sprites[0];
            panels[0].sprite.GetComponent<Image>().rectTransform.sizeDelta = obj.sprite_size;
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
            panels[2].sprite.GetComponent<Image>().rectTransform.sizeDelta = obj.sprite_size;
            if (obj.sprites.Length > 1) //maybe the object does not need to "change sprites" EX: signing contract
            {
                panels[2].message_text.text = obj.messages[0];
                panels[2].button.SetActive(true);
            }
            else
            {
                panels[2].button.SetActive(false);
            }
            if (obj.sprite_index == obj.sprites.Length - 1) //hide the change sprites button because all sprites have been used
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
            yield return new WaitForSecondsRealtime(textSpeed);
        }
        if (index < obj.messages.Length - 1)
        {
            if(obj.option_messages.Length == 0 || (option_index <= obj.when_show_options.Length - 1 && obj.when_show_options[option_index] != index))
            {
                continue_notice_text.SetActive(true);
            }
        }
        panelInteraction.currently_optioning = false;
        if (obj.option_messages.Length > 0) //if the player has to select an option
        {
            //show options
            int i = -1;
            if((option_index < obj.when_show_options.Length) && (obj.when_show_options[option_index] == index)) //when it reaches a point to show an option
            {
                Cursor.lockState = CursorLockMode.None; //allow player to move cursor
                Cursor.visible = true;
                panelInteraction.currently_optioning = true;
                option_index++;
                do //show how many options are available
                {
                    i++;
                    option_message_index++;
                    option_boxes[i].SetActive(true);
                    foreach(GameObject box in option_boxes) //move boxes based on how many options are available
                    {
                        Vector3 newPos = box.transform.localPosition;
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        for (int i = 0; i < 3; i++) //move boxes back
        {
            option_boxes[i].transform.localPosition = option_positions[i].transform.localPosition;
            option_boxes[i].SetActive(false);
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
    }

    public void ChooseOption(int i)
    {
        answer_text.text = string.Empty; //clear out text first
        if (obj.correct_answers[option_index - 1] == i)
        {
            answer.GetComponent<Image>().sprite = correct;
            answer_text.color = Color.green;
        }
        else
        {
            answer.GetComponent<Image>().sprite = wrong;
            answer_text.color = Color.red;
            answer_text.text = "You chose: " + obj.option_messages[(option_message_index - 1) - (3 - i)] + "\nCorrect Answer: ";
        }
        for (int x = 0; x < 3; x++) //hide boxes to prevent repeated clicking of ChooseOption()
        {
            option_boxes[x].SetActive(false);
        }
        answer_text.text += obj.option_messages[(option_message_index - 1) - (3 - obj.correct_answers[option_index - 1])]; //show the correct answer
        StartCoroutine(FadeImage(true));
    }

    public IEnumerator FadeImage(bool fadeOut)
    {
        answer.GetComponent<Image>().color = new Color(1, 1, 1, fadeTime);
        answer_text.color = new Color(fadeTime, answer_text.color.g, answer_text.color.b, fadeTime); //show the image and text for a while before fading out (if you want to fade in change the code, right now we only need fade out)
        yield return new WaitForSecondsRealtime(1);
        if (fadeOut)
        {
            for (float i = fadeTime; i >= -0.1; i -= Time.deltaTime) //loop over fadeTime
            {
                answer.GetComponent<Image>().color = new Color(1, 1, 1, i);
                answer_text.color = new Color(answer_text.color.r, answer_text.color.g, answer_text.color.b, i);
                yield return null;
            }
        }
        else
        {
            for (float i = -0.1f; i <= fadeTime; i += Time.deltaTime)
            {
                answer.GetComponent<Image>().color = new Color(1, 1, 1, i);
                answer_text.color = new Color(answer_text.color.r, answer_text.color.g, answer_text.color.b, i);
                yield return null;
            }
        }
        yield return new WaitForSecondsRealtime(0.3f); //wait some time before changing the screen
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
        panelInteraction.currently_optioning = false;
        continue_notice_text.SetActive(false);
        interaction_notice_text.SetActive(false);
        index = -1; //shows that there is no more text left
        panels[0].panel_object.SetActive(false);
        panels[1].panel_object.SetActive(false);
        panels[2].panel_object.SetActive(false);
        if (obj.item_slot == 2) //the clean up level
        {
            gameObject.GetComponent<CleanUp>().BeginCleanUp();
            obj.gameObject.tag = "Untagged";    //to prevent press SPACE from showing
        }
        else if (obj.item_slot != -99)
        {
            checkList.AddItem(obj.item_slot);
        }
        if (obj.hide_object) //maybe we want to hide something
        {
            obj.transform.parent.gameObject.SetActive(false); //the interact cube is ALWAYS attached to an object
        }
    }

}
