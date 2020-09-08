using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PS4Binds", menuName = "Scriptables/Controllers/PS")]
public class PS4 : ScriptableObject
{
    //access
    public int instance;
    public string nameVal;

    //buttons
    public string XBtn;
    public string CircBtn;
    public string SqrBtn;
    public string TriBtn;
    public string L1;
    public string R1;

            //Triggers
    public string R2;
    public string L2;

            //DPad
    public string DLeft;
    public string DRight;
    public string DUp;
    public string DDown;

    //Thumbsticks
    public string thumbVert;
    public string thumbHoriz;
}
