using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class NpcRequest : MonoBehaviour
{
    public enum AIState { Queue, Flee, Fight};
    public AIState state;

    public Canvas Bubble;

    //Timer for checking if can move up in queue
    public float waitTimerMax = 2;
    float waitTimer;
    

    //Timer checking if time is up and AI should leave
    public float timerMax;
    public float timer = 0;
    bool runTimer;

    public bool GotWeapon = false;

    public float speed = 4f;
    public float startTime;

    [SerializeField] NavMeshAgent agent;

    public float maxRange = 2;

    [SerializeField] Weapon.weaponType weapon;

    [SerializeField] GameObject battlePos;
    [SerializeField] GameObject fleePos;
    public GameObject GoalQueuePos;
    [SerializeField] GameObject CurrentQueuePos;
    public int placeInQueue;

    [SerializeField] Image _sword;
    [SerializeField] Image _axe;
    [SerializeField] Image _activeWeapon;

    [SerializeField] Slider _slide;


    [SerializeField] ParticleSystem anger;

    Vector3 CurRotate;
    Vector3 faceNorth;

    [SerializeField] Transform parentPos;

    bool alive;
    NPCSpawner parentScript;

    public void Start()
    {
        parentScript = GetComponentInParent<NPCSpawner>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        updateQueuePos();
        fleePos = parentScript.fleePos;
        battlePos = parentScript.battlePos;

        faceNorth = new Vector3(0, Input.compass.trueHeading, 0);

        agent.SetDestination(CurrentQueuePos.transform.position);
        state = AIState.Queue;

        Bubble.gameObject.SetActive(false);

        _slide.maxValue = timerMax;

        alive = true;

        switch (weapon)
        {
            case Weapon.weaponType.Sword:
                _activeWeapon = _sword;
                break;

            case Weapon.weaponType.Axe:
                _activeWeapon = _axe;
                break;
        }
    }


    private void OnTriggerEnter(Collider col) //Trigger Zone Near benches
    {
        if (col.tag == "requestZone")
        {
            Bubble.gameObject.SetActive(true);
            _activeWeapon.gameObject.SetActive(true);
            runTimer = true;
        }

        if (col.gameObject.GetComponent<Weapon>() != null)
        {
            if (col.gameObject.GetComponent<Weapon>().myWeapon == weapon)
            {
                //col.GetComponentInParent<Interact>().heldObj = null;
                GotWeapon = true;
                col.gameObject.transform.parent = parentPos;
                col.gameObject.transform.localPosition = new Vector3(0, 3.5f, 0);
                col.gameObject.transform.rotation = Quaternion.Euler(x: -90, y: +0, z: +90);
                //Destroy(col.gameObject);
            }
        }


    }

    private void Update()
    {
        _slide.value = timer;

        //Start running timer when AI enters range of counter
        if (runTimer)
        {
            timer += Time.deltaTime;
        }
        if (timer >= timerMax)
        {
            state = AIState.Flee;
            timer -= timerMax;
            Bubble.gameObject.SetActive(false);
        }

        if (GotWeapon == true)
        {
            state = AIState.Fight;
        }

        switch (state)
        {
            case AIState.Queue:
                QueueFunc();
                break;
            case AIState.Flee:
                FleeFunc();
                break;

            case AIState.Fight:
                FightFunc();
                break;
        }

    }


    public float lerpTime;
    void QueueFunc()
    {
        if (agent.remainingDistance > maxRange)
        {
            agent.SetDestination(CurrentQueuePos.transform.position);
        }
        else
        {
            waitTimer -= Time.deltaTime;

            lerpTime += Time.deltaTime;
            lerpTime = Mathf.Clamp(lerpTime, 0, 1);
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(CurRotate), Quaternion.Euler(faceNorth), lerpTime);

            if (waitTimer <= 0)
            {
                waitTimer += waitTimerMax;
                updateQueuePos();
            }
            if (CurRotate != faceNorth)
            {
                lerpTime = 0;
                CurRotate = transform.rotation.eulerAngles;
            }

        }
    }

    void FleeFunc()
    {
        agent.SetDestination(fleePos.transform.position);
        Bubble.gameObject.SetActive(false);
        RemoveMe();

        Debug.Log("Fleeing");
    }


    void FightFunc()
    {
        agent.SetDestination(battlePos.transform.position);
        Bubble.gameObject.SetActive(false);
        RemoveMe();

        Debug.Log("Fighting");
    }

    //Invoke when Fleeing/Battling for 5 seconds
    void RemoveMe()
    {
        if (alive)
        {
            parentScript.listRemove(gameObject);
        }
        alive = false;
    }

    public void updateQueuePos()
    {
        CurrentQueuePos = GoalQueuePos;
        agent.SetDestination(CurrentQueuePos.transform.position);
    }

}
