using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordNPC : NPC
{
    float maxDist = 1;

    public override void LeaveBattle()
    {
        if (agent.destination != fleeSite.transform.position)
        {
            agent.SetDestination(fleeSite.transform.position);
        }
    }

    public override void JoinBattle()
    {
        if (agent.destination != battleSite.transform.position)
        {
            agent.SetDestination(battleSite.transform.position);
        }
    }

    public override void JoinQueue()
    {
        if (agent.destination != requestQueue[placeInQueue].transform.position)
        {
            agent.SetDestination(requestQueue[placeInQueue].transform.position);
        }
    }

    //START CALLED IN NPC BASE CLASS


    void Update()
    {
        //Choose Current Behaviour
        switch (activeBehaviour)
        {
            case AIState.QueueUp:

                JoinQueue();
                break;

            case AIState.Wait:
                runTimer();
                break;

            case AIState.Fight:

                JoinBattle();
                break;

            case AIState.Flee:
                
                LeaveBattle();
                break;
        }

    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Weapon>().myWeapon == desiredWeapon && col.gameObject.GetComponent<Weapon>().completed)
        {
            activeBehaviour = AIState.Fight;
        }
    }

    float updateTimer = 2;
    void runTimer()
    {
        updateTimer -= Time.deltaTime;

        if (updateTimer <= 0)
        {
            updateTimer = 2;
            JoinQueue();
        }
    }


}
