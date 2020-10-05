using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{
    public enum metals { Tin, Copper, Bronze}
    public metals metal;



    public metals desiredMetal()
    {
        return metal;
    }
}
