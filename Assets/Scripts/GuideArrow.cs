using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Fall 2023:
 * A script to move the arrow and keep it looking at the player
 * 
 * CheckListScript uses this to move the arrow
 * ShowHideArrow() is not being used
 */
public class GuideArrow : MonoBehaviour
{
    [SerializeField]
    GameObject Player, arrow;

    void Update()
    {
        arrow.transform.LookAt(Player.transform.position);
    }

    public void MoveArrow(GameObject obj)
    {
        arrow.transform.position = obj.transform.position;
    }

    public void ShowHideArrow(bool show) //not used
    {
        if (show)
        {
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);
        }
    }
}
