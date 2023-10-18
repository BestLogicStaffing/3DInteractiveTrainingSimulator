using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Fall 2023:
 * Script for the CheckList or TaskList (whatever you call it) in the Canvas
 * You can check boxes, but not uncheck them (why would you need to)
 * It checks what item has been added and checks a box (it does not keep the information of the item after this)
 * When an item is added it increases an integer which will do something once we have an ending
 */

public class CheckListScript : MonoBehaviour
{
    [SerializeField]
    GameObject checkbox_1, checkbox_2, checkbox_3, checkbox_4, checkbox_5;

    [SerializeField]
    Sprite checkbox_checked;

    [SerializeField]
    GameObject[] arrow_locations;
    GuideArrow arrow;

    public bool checkListAppeared = false;
    int total_items = 0;

    private void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        arrow = gameObject.GetComponent<GuideArrow>();
    }

    public void AddItem(string i)
    {
        if (i == "CheckList") //make it appear
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            checkListAppeared = true;
            arrow.MoveArrow(arrow_locations[0]);
        }
        if (checkListAppeared) //just to make sure that the checkList has appeared first before you can check off items
        {
            total_items++;
            if (i == "Contract")
            {
                checkbox_1.GetComponent<Image>().sprite = checkbox_checked;
            }
            else if (i == "2")
            {
                checkbox_2.GetComponent<Image>().sprite = checkbox_checked;
            }
            else if(i == "3")
            {
                checkbox_3.GetComponent<Image>().sprite = checkbox_checked;
            }
            else if (i == "4")
            {
                checkbox_4.GetComponent<Image>().sprite = checkbox_checked;
            }
            else if (i == "5")
            {
                checkbox_5.GetComponent<Image>().sprite = checkbox_checked;
            }
            else
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
