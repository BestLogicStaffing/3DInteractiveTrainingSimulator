using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * the panels in the canvas
 * EXAMPLE: Panel 0 has a name, message, and picture
 */

public class PanelClass : MonoBehaviour
{
    public int panel_type; //0 for one with image, 1 with no image
    public GameObject sprite;
    public GameObject panel_object, name_box;
    public TMP_Text name_text, message_text;

}
