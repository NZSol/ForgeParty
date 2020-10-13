using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class NpcRequest : MonoBehaviour
{
    public enum AIState { Queue, Wait, Flee, Fight};
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

    NavMeshAgent agent;

    float dist;
    public float maxRange = 2;

    [SerializeField] Weapon.weaponType weapon;

    [SerializeField] GameObject battlePos;
    [SerializeField] GameObject fleePos;
    [SerializeField] GameObject QueuePos;

    [SerializeField] Image _sword;
    [SerializeField] Image _axe;
    [SerializeField] Image _activeWeapon;

    [SerializeField] Slider _slide;


    [SerializeField] ParticleSystem anger;

    Vector3 CurRotate;
    Vector3 faceNorth;

    [SerializeField] Transform parentPos;

    private void Start()
    {
        faceNorth = new Vector3(0, Input.compass.trueHeading, 0);

        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(QueuePos.transform.position);
        state = AIState.Queue;

        Bubble.gameObject.SetActive(false);

        _slide.maxValue = timerMax;

        switch (weapon)
        {
            case Weapon.weaponType.Sword:
                _activeWeapon = _sword;
                break;

            case Weapon.weaponType.Axe:
                _activeWeapon = _axe;
                break;
        }
        //anger.gameObject.SetActive(false);
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
                col.GetComponentInParent<Interact>().heldObj = null;
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
        //Get Distance from target destination
        dist = Vector3.Distance(new Vector3(agent.destination.x, transform.position.y, agent.destination.z), transform.position);
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

            case AIState.Wait:
                WaitFunc();
                break;

            case AIState.Flee:
                FleeFunc();
                break;

            case AIState.Fight:
                FightFunc();
                break;
        }

    }

    void QueueFunc()
    {
        if (dist > maxRange)
        {
            agent.SetDestination(QueuePos.transform.position);
        }
        else
        {
            if (CurRotate != faceNorth)
            {
                lerpTime = 0;
                CurRotate = transform.rotation.eulerAngles;
            }

            state = AIState.Wait;
            waitTimer = waitTimerMax;
        }
    }

    public float lerpTime;
    void WaitFunc()
    {
        waitTimer -= Time.deltaTime;

        lerpTime += Time.deltaTime;
        lerpTime = Mathf.Clamp(lerpTime, 0, 1);
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(CurRotate), Quaternion.Euler(faceNorth), lerpTime);


        if (waitTimer <= 0)
        {
            waitTimer += waitTimerMax;
            if (dist > maxRange)
            {
                state = AIState.Queue;
            }
        }

    }


    void FleeFunc()
    {
        agent.SetDestination(fleePos.transform.position);
        Bubble.gameObject.SetActive(false);
        //if (anger.gameObject.activeSelf != true)
        //{
        //    anger.gameObject.SetActive(true);
        //}

        Debug.Log("Fleeing");
    }


    void FightFunc()
    {
        agent.SetDestination(battlePos.transform.position);
        Bubble.gameObject.SetActive(false);

        Debug.Log("Fighting");
    }

}
