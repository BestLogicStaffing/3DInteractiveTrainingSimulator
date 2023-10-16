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
        Vector3 position = obj.transform.position;
        position.y += 1.5f;
        arrow.transform.position = position;
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
