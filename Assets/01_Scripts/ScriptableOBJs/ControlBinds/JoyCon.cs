using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SwitchBinds", menuName = "Scriptables/Controllers/Switch")]
public class JoyCon : ScriptableObject
{
            //access
    public int instance;
    public string nameVal;

            //buttons
    public string ABtn;
    public string BBtn;
    public string XBtn;
    public string YBtn;
    public string L;
    public string R;

            //Triggers
    public string ZR;
    public string ZL;

            //DPad
    public string DLeft;
    public string DRight;
    public string DUp;
    public string DDown;

    //Thumbsticks
    public string thumbVert;
    public string thumbHoriz;
}
