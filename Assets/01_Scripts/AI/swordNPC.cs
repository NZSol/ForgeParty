using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordNPC : NPC
{
    //START CALLED IN NPC BASE CLASS


    void Update()
    {
        if (!hasWeapon)
        {
            if (!inPlace)
            {
                agent.destination = requestQueue[placeInQueue].transform.position;
            }
        }
    }
}
