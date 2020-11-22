using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class NpcRequest : MonoBehaviour
{
    GameObject teamVariables;

    public enum AIState { Queue, Flee, Fight};
    public AIState state = new AIState();

    public Canvas Bubble = null;

    //Timer for checking if can move up in queue
    public float waitTimerMax = 2;
    public float waitTimer = 0;
    

    //Timer checking if time is up and AI should leave
    public float timerMax = 0;
    public float timer = 0;
    bool runTimer = false;

    public bool GotWeapon = false;

    public float speed = 4f;
    public float startTime = 0;

    [SerializeField] NavMeshAgent agent = null;

    public float maxRange = 2;

    [SerializeField] Weapon.weaponType weapon = new Weapon.weaponType();

    [SerializeField] GameObject battlePos = null;
    [SerializeField] GameObject fleePos = null;
    public GameObject GoalQueuePos = null;
    [SerializeField] GameObject CurrentQueuePos = null;
    public int placeInQueue = 0;

    [SerializeField] Image _sword = null;
    [SerializeField] Image _axe = null;
    [SerializeField] Image _activeWeapon = null;

    [SerializeField] Slider _slide = null;


    [SerializeField] ParticleSystem anger;

    Vector3 CurRotate = Vector3.zero;
    Vector3 faceNorth = Vector3.zero;

    [SerializeField] Transform parentPos = null;

    
    public GameObject myTeamList;

    bool alive = true;
    NPCSpawner parentScript;
    Animator anim;

    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        teamVariables = GameObject.FindWithTag("LevelGod");
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

    [SerializeField] GameObject heldWeapon = null;
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
            if (col.gameObject.GetComponent<Weapon>().myWeapon == weapon && col.gameObject.GetComponent<Weapon>().completed)
            {
                col.GetComponentInParent<Interact>().heldObj = null;
                GotWeapon = true;
                col.gameObject.transform.parent = parentPos;
                col.gameObject.transform.localPosition = new Vector3(0, 3.5f, 0);
                col.gameObject.transform.rotation = Quaternion.Euler(x: -90, y: +0, z: +90);
                heldWeapon = col.gameObject;
                heldWeapon.GetComponent<WeaponVars>().setVar();
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

            //Moves blend from point a to c smoothly based on bubble progression
            var timerPercent = (timer / timerMax) * 1;
            anim.SetFloat("Blend", timerPercent);
        }
        if (timer >= timerMax)
        {
            state = AIState.Flee;
            timer -= timerMax;
            agent.SetDestination(fleePos.transform.position);
            anim.SetBool("Moving", true);
            Bubble.gameObject.SetActive(false);
        }

        if (GotWeapon == true)
        {
            state = AIState.Fight;
            agent.SetDestination(battlePos.transform.position);
            anim.SetBool("Moving", true);
            Bubble.gameObject.SetActive(false);
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


    public float lerpTime = 0;
    void QueueFunc()
    {
        if (agent.remainingDistance > maxRange)
        {
            agent.SetDestination(CurrentQueuePos.transform.position);
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
            waitTimer -= Time.deltaTime;

            lerpTime += (Time.deltaTime / 5);
            lerpTime = Mathf.Clamp(lerpTime, 0, 1);
            CurRotate = transform.rotation.eulerAngles;

            if (waitTimer <= 0)
            {
                waitTimer += waitTimerMax;
                updateQueuePos();
            }

            if (CurRotate != faceNorth)
            {
                transform.rotation = Quaternion.Lerp(Quaternion.Euler(CurRotate), Quaternion.Euler(faceNorth), lerpTime);
            }
            else
            {
                lerpTime = 0;
            }
        }
    }

    void FleeFunc()
    {
        Bubble.gameObject.SetActive(false);
        RemoveMe();

        if (agent.pathPending)
        {
        }
        else if (agent.remainingDistance < maxRange)
        {
            Destroy(gameObject);
        }

        Debug.Log("Fleeing");
    }


    void FightFunc()
    {
        RemoveMe();

        if (agent.pathPending)
        {
        }
        else if (agent.remainingDistance < maxRange)
        {
            heldWeapon.GetComponentInChildren<MeshRenderer>().enabled = false;
            heldWeapon.GetComponent<WeaponVars>().inFight = true;
            heldWeapon.transform.SetParent(myTeamList.transform);
            Destroy(gameObject);
        }
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
