using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ControllerInit : MonoBehaviour
{
            //Controller Binds
    [SerializeField] XB1 xboxBinds;
    [SerializeField] PS4 ps4Binds;
    [SerializeField] JoyCon switchBinds;

            //Players


            //Controller Inputs
    List<string> joyList = new List<string>();

    string[] joyArray;

            //PlayerBinds
    public bool P1Bind = false;
    public bool P2Bind = false;
    public bool P3Bind = false;
    public bool P4Bind = false;

    public int i = 0;


            //Detect all connected controllers
    void Start()
    {
        Invoke("checkInputs", 2);

        foreach (string val in joyList)
        {
            if (val == "Controller (Xbox One For Windows)")
            {
            }
        }
    }



    void Update()
    {
        if (P1Bind == false)
        {
            if (Input.GetButtonDown(xboxBinds.ABtn))
            {
                Debug.Log("Player 1 bound");
                P1Bind = true;

                //disable this controllers inputs
            }
        }

        else if (P2Bind == false)
        {
            if (Input.GetButtonDown("Interact_1"))
            {
                Debug.Log("Player 2 bound");
                P2Bind = true;
                //disable this controllers inputs
            }
        }

        else if (P3Bind == false)
        {
            if (Input.GetButtonDown("Interact_2"))
            {
                P3Bind = true;
                Instantiate(xboxBinds);
                //disable this controllers inputs
            }
        }

        else if (P4Bind == false)
        {
            if (Input.GetButtonDown(xboxBinds.ABtn))
            {
                P4Bind = true;
                Instantiate(xboxBinds);
                //disable this controllers inputs
            }
        }


    }



            //check for new/removed controllers and summon call two seconds later
    void checkInputs()
    {
        print("checking inputs");
        joyArray = Input.GetJoystickNames();
        if (joyArray.Count() != joyList.Count)
        {
            joyList = joyArray.ToList();
        }
        else
        {
            Invoke("checkInputs", 2);
        }
    }

}
