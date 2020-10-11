using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPC : MonoBehaviour
{
                        //POSITIONS TO MOVE TOWARDS
    [SerializeField] protected Transform battleSite;
    [SerializeField] protected Transform fleeSite;
    [SerializeField] protected GameObject[] requestQueue;

                        //NAVMESH AGENT
    [SerializeField] protected NavMeshAgent agent;
                        //LOOK FOR WEAPON TYPE ON WEAPON
    [SerializeField] protected Weapon.weaponType desiredWeapon;
                        //AI STATEMACHINE
    [SerializeField] protected enum AIState { QueueUp, Wait, Flee, Fight}
    [SerializeField] protected AIState activeBehaviour;

                        //TIMER CHECKING IF OUT OF TIME
    [SerializeField] protected float timer;
    [SerializeField] protected float timerMax;
                        //QUEUE
    protected Queue<GameObject> npcQueue = new Queue<GameObject>();
    protected int placeInQueue = 0;

    protected void Start()
    {
        requestQueue = GameObject.FindGameObjectsWithTag("Queue");
        agent = gameObject.GetComponent<NavMeshAgent>();
        timerMax = 10;
        npcQueue.Enqueue(gameObject);
        placeInQueue = npcQueue.Count;

        JoinQueue();
    }

    public abstract void LeaveBattle();

    public abstract void JoinQueue();

    public abstract void JoinBattle();

}
