using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public List<PanelClass> panels;
    public GameObject interaction_notice_text;

    public void Interact(int panel_type, Sprite sprite, string name_text, string message_text)
    {
        if (panel_type == 0) //panel with image
        {
            panels[0].panel_object.SetActive(true);
            panels[0].sprite.GetComponent<Image>().sprite = sprite;
            panels[0].name_text.text = name_text;
            panels[0].message_text.text = message_text;
        }
        else //panel with no image
        {
            panels[1].panel_object.SetActive(true);
            panels[1].message_text.text = message_text;
        }
    }

    public void Disinteract()
    {
        panels[0].panel_object.SetActive(false);
        panels[1].panel_object.SetActive(false);
    }
}
