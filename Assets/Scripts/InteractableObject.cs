using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * an object the player can interact with to display on a PanelClass
 * EXAMPLE: Chair has InteractableObject of panel_type = 0, which will show a name, picture, and message
 */

public class InteractableObject : MonoBehaviour
{
    public int panel_type; //0 for one with image, 1 with no image
    public Sprite sprite;
    public string name_text;
    public string[] messages;

}
