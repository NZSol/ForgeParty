﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "XB1Binds", menuName = "Scriptables/Controllers/Xbox")]
public class XB1 : ScriptableObject
{
    //access
    public int instance;
    public string nameVal;

    //buttons
    public string ABtn;
    public string BBtn;
    public string XBtn;
    public string YBtn;
    public string LB;
    public string RB;

            //Triggers
    public string RT;
    public string LT;

            //DPad
    public string DLeft;
    public string DRight;
    public string DUp;
    public string DDown;

    //Thumbsticks
    public string thumbVert;
    public string thumbHoriz;
}
