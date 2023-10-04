using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckListScript : MonoBehaviour
{
    [SerializeField]
    GameObject checkbox_1, checkbox_2, checkbox_3, checkbox_4, checkbox_5;

    [SerializeField]
    Sprite checkbox_checked;

    public bool has_one, has_two, has_three, has_four, has_five;

    void Update()
    {
        if (has_one)
        {
            checkbox_1.GetComponent<Image>().sprite = checkbox_checked;
        }
        if (has_two)
        {
            checkbox_2.GetComponent<Image>().sprite = checkbox_checked;
        }
        if (has_three)
        {
            checkbox_3.GetComponent<Image>().sprite = checkbox_checked;
        }
        if (has_four)
        {
            checkbox_4.GetComponent<Image>().sprite = checkbox_checked;
        }
        if (has_five)
        {
            checkbox_5.GetComponent<Image>().sprite = checkbox_checked;
        }
    }

    public void GetItem(int i)
    {
        if(i == 1)
        {
            has_one = true;
        }
        if (i == 2)
        {
            has_two = true;
        }
        if (i == 3)
        {
            has_three = true;
        }
        if (i == 4)
        {
            has_four = true;
        }
        if (i == 5)
        {
            has_five = true;
        }
    }
}
