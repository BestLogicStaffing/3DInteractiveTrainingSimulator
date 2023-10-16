using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Fall 2023:
 * an object the player can interact with to display on a PanelClass
 * EXAMPLE: Chair has InteractableObject of panel_type = 0, which will show a name, picture, and message
 */

public class InteractableObject : MonoBehaviour
{
    public int panel_type; //0 for one with image, 1 with no image, 2 as just image

    public Sprite sprite; //a picture to show on panel_type = 0 and 2
    public string name_text;
    public string[] messages;

    public bool has_options; //will the player have to choose an option?
    public int[] when_show_options; //how many messages does the player have to read before choosing an option? (MUST BE WITHIN MESSAGES LENGTH)
    public string[] option_messages; //the text on the options (leave a message blank if you want multiple options in 1 conversation)

    public string panel2_button_text; //text that appears on panel 2's button
    public Sprite on_click_sprite; //if the sprite changes after clicking on the button

}
