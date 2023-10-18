using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Fall 2023:
 * the panels are in the canvas
 * Panel 0 has a name, message, and picture
 * Panel 1 has only message
 * Panel 2 has only picture
 * 
 * message_text is used for all panels, but panel 2 uses it for its button text
 */

public class PanelClass : MonoBehaviour
{
    public int panel_type; //0 for one with image, 1 with no image
    public GameObject panel_object, sprite; //panel_object is the actual panel containing the sprite, name_text, and message_text.
                                            //sprite is the picture that is displayed
    public TMP_Text name_text, message_text; //where the name and messages will appear

    public GameObject button; //to turn on or off the button
}
