using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Fall 2023:
 * an object the player can interact with to display on a PanelClass
 * EXAMPLE: Chair has InteractableObject of panel_type = 0, which will show a name, picture, and message
 * if panel_type = 0 or 1, a message is required
 * sprite_size is REQUIRED to view the image. It must be the size of ALL the sprites in this InteractableObject
 */

public class InteractableObject : MonoBehaviour
{
    public int panel_type; //0 for one with image, 1 with no image, 2 as just image

    public Sprite[] sprites; //a picture to show on panel_type = 0 and 2
    public Vector2 sprite_size; //AGAIN: must be the size of ALL the sprites in this InteractableObject
    public int sprite_index = 0;

    public string name_text;
    public string[] messages;

    public int[] when_show_options; //how many messages does the player have to read before choosing an option? (MUST BE WITHIN MESSAGES LENGTH)
    public string[] option_messages; //the text on the options (leave a message blank if you want multiple options in 1 conversation)
    public int[] correct_answers; //a list of what the correct answers are

    public int item_slot; //what item number is it / what order is it in the game?
    public bool hide_object; //should the gameObject SetActive(false)?
}
