using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Fall 2023:
 * Script for the CheckList or TaskList (whatever you call it) in the Canvas
 * You can check boxes, but not uncheck them (why would you need to)
 * It checks what item has been added and checks a box (it does not keep the information of the item after this)
 * When an item is added it increases an integer which will do something once we have an ending
 * 
 * Reserved numbers: -1 for CheckList, 0 for Sign Contract, 1 for Virtual Call, 2 for Clean Up, -99 for anything else
 */

public class CheckListScript : MonoBehaviour
{
    [SerializeField]
    GameObject checkbox_1, checkbox_2, checkbox_3, checkbox_4, checkbox_5;

    [SerializeField]
    Sprite checkbox_checked;

    [SerializeField]
    TMP_Text information_text;

    [SerializeField]
    GameObject[] arrow_locations;
    GuideArrow arrow;

    [HideInInspector]
    public bool checkListAppeared = false;

    public int total_items = 0;

    private void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        arrow = gameObject.GetComponent<GuideArrow>();
    }

    public void AddItem(int i)
    {
        if (i == -1) //make CheckList appear
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            checkListAppeared = true;
            information_text.text = "Go to the HR office and sign your contract";
            arrow.MoveArrow(arrow_locations[0]);
        }
        else if (checkListAppeared) //just to make sure that the checkList has appeared first before you can check off items
        {
            total_items++;
            if (i == 0)
            {
                checkbox_1.GetComponent<Image>().sprite = checkbox_checked;
                information_text.text = "Engage in conversations with virtual colleagues";
                arrow.MoveArrow(arrow_locations[1]);
            }
            else if (i == 1)
            {
                checkbox_2.GetComponent<Image>().sprite = checkbox_checked;
                information_text.text = "Clean up the work place";
                arrow.MoveArrow(arrow_locations[2]);
            }
            else if(i == 2)
            {
                checkbox_3.GetComponent<Image>().sprite = checkbox_checked;
            }
            else if (i == 3)
            {
                checkbox_4.GetComponent<Image>().sprite = checkbox_checked;
            }
            else if (i == 4)
            {
                checkbox_5.GetComponent<Image>().sprite = checkbox_checked;
            }
            else if (i != -1) //tried to add an item that does not exist (excluding CheckList)
            {
                Debug.Log($"this item ({i}) does not exist so it cannot be added to CheckList");
            }

            if(total_items == 5)
            {
                Debug.Log("you got all the items");
            }
        }
        else
        {
            Debug.Log($"check list has not appeared, but trying to get {i}");
        }
        
    }
}
