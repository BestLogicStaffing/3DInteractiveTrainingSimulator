using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ShowHideArrow(bool show)
    {
        //switch the arrow to hide or show
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
