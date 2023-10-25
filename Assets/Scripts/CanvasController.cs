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
    [SerializeField] int option_index, option_message_index; //which options is it on
    public GameObject interaction_notice_text, continue_notice_text;
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
            yield return new WaitForSeconds(textSpeed);
        }

        if (index < obj.messages.Length - 1)
        {
            if(obj.option_messages.Length == 0 || (obj.option_messages.Length > 0 && option_index <= obj.when_show_options.Length - 1 && obj.when_show_options[option_index] != index))
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        yield return new WaitForSeconds(1);
        if (fadeOut)
        {
            for (float i = fadeTime; i >= 0; i -= Time.deltaTime) //loop over fadeTime
            {
                answer.GetComponent<Image>().color = new Color(1, 1, 1, i);
                answer_text.color = new Color(answer_text.color.r, answer_text.color.g, answer_text.color.b, i);
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i <= fadeTime; i += Time.deltaTime)
            {
                answer.GetComponent<Image>().color = new Color(1, 1, 1, i);
                answer_text.color = new Color(answer_text.color.r, answer_text.color.g, answer_text.color.b, i);
                yield return null;
            }
        }
        yield return new WaitForSeconds(0.3f); //wait some time before changing the screen
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
