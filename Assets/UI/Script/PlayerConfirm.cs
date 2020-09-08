using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConfirm : MonoBehaviour
{
    public bool Player1click;
    public bool Player2click;
    public Image idle1;
    public Image idle2;
    public Image confirmed;
 

    // Update is called once per frame
    void Update()
    {
        if (Player1click == true)
        {
            idle1.color = confirmed.color;
                }
        else
        {
            idle1.color = idle1.color;//need to explain that is the initial color
        }

        if (Player2click == true)
        {
            idle2.color = confirmed.color;
        }
        else
        {
            idle2.color = idle2.color;//need to explain that is the initial color

        }
        
    }

   public void Player1Confirmed()
    {
        Player1click = true;
        Debug.Log("you made it");
    }
    public void Player2Confirmed()
    {
        Player2click = true;
        Debug.Log("ready to go");
    }


    public void Back()
    {
        Player1click = false;
        Player2click = false;
    }
}
