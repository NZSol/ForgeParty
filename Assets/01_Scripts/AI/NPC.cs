using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPC : MonoBehaviour
{
    [SerializeField] protected Transform battleSite;
    [SerializeField] protected Transform fleeSite;
    [SerializeField] protected GameObject[] requestQueue;

    [SerializeField] protected NavMeshAgent agent;

    [SerializeField] protected enum desiredWeapon { Sword, Axe, Spear, Bow }
    [SerializeField] protected desiredWeapon myDesiredWeapon;

    [SerializeField] protected float timer;
    [SerializeField] protected float timerMax;

    protected bool hasWeapon;
    protected bool inPlace;

    protected Queue<GameObject> npcQueue = new Queue<GameObject>();
    protected int placeInQueue = 0;

    protected void Start()
    {
        requestQueue = GameObject.FindGameObjectsWithTag("Queue");
        agent = gameObject.GetComponent<NavMeshAgent>();
        timerMax = 10;
        npcQueue.Enqueue(gameObject);
        placeInQueue = npcQueue.Count;
    }
}
