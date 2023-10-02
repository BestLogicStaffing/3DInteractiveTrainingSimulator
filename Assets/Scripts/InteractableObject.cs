using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * an object the player will interact with to display on PanelClass
 * EXAMPLE: Chair
 */

public class InteractableObject : MonoBehaviour
{
    public int panel_type; //0 for one with image, 1 with no image
    public Sprite sprite;
    public string name_text, message_text;
}
