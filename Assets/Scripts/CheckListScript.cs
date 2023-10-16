using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Fall 2023:
 * Script for the CheckList or TaskList (whatever you call it) in the Canvas
 * You can check boxes, but not uncheck them (why would you need to)
 * It looks through a list to see if the item has been added and sets the sprite to checked
 * A string list is just easier to manage
 */

public class CheckListScript : MonoBehaviour
{
    [SerializeField]
    GameObject checkbox_1, checkbox_2, checkbox_3, checkbox_4, checkbox_5;

    [SerializeField]
    Sprite checkbox_checked;

    [SerializeField]
    GameObject[] arrow_locations;

    public List<string> items;
    GuideArrow guide;

    private void Start()
    {
        guide = gameObject.GetComponent<GuideArrow>();
        guide.MoveArrow(arrow_locations[0]);
    }

    void Update()
    {
        if (items.Contains("1"))
        {
            checkbox_1.GetComponent<Image>().sprite = checkbox_checked;
        }
        if (items.Contains("2"))
        {
            checkbox_2.GetComponent<Image>().sprite = checkbox_checked;
        }
        if (items.Contains("3"))
        {
            checkbox_3.GetComponent<Image>().sprite = checkbox_checked;
        }
        if (items.Contains("4"))
        {
            checkbox_4.GetComponent<Image>().sprite = checkbox_checked;
        }
        if (items.Contains("5"))
        {
            checkbox_5.GetComponent<Image>().sprite = checkbox_checked;
        }
    }

    public void AddItem(string i)
    {
        items.Add(i);
    }
}
