using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class NpcRequest : MonoBehaviour
{
    public Canvas Bubble;
    public Slider Timer;
    
    public bool GotWeapon;
    public bool runTimer = false;

    public float speed = 4f;
    public float startTime;

    NavMeshAgent agent;

    private bool leaving;

    [SerializeField] Weapon.weaponType weapon;

    [SerializeField] GameObject battlePos;
    [SerializeField] GameObject fleePos;
    [SerializeField] GameObject QueuePos;

    [SerializeField] Image _sword;
    [SerializeField] Image _axe;

    [SerializeField] Image _activeWeapon;


    [SerializeField] MeshFilter AxeMan;
    [SerializeField] MeshFilter Swordman;

    [SerializeField] MeshFilter ActiveModel;

    private void Start()
    {


        agent = GetComponent<NavMeshAgent>();




        Bubble.enabled = false;
       
        GotWeapon = false;

        //Declare which weapon NPC wants and set active Image to weapon type
        weapon = (Weapon.weaponType)Random.Range(1, 3);
        switch (weapon)
        {
            case Weapon.weaponType.Sword:
                _activeWeapon = _sword;
                ActiveModel = Swordman;
                break;

            case Weapon.weaponType.Axe:
                _activeWeapon = _axe;
                ActiveModel = AxeMan;
                break;
        }


        gameObject.GetComponent<MeshFilter>().mesh = ActiveModel.mesh;
    }

    private void OnTriggerEnter(Collider col) //Trigger Zone Near benches
    {
        if (col.tag == "requestZone")
        {
            Bubble.enabled = true;
            _activeWeapon.gameObject.SetActive(true);
            runTimer = true;
        }

        //if (col.gameObject.GetComponent<Weapon>().myWeapon == Weapon.weaponType.Sword)
        //{
        //    GotWeapon = true;
        //    Destroy(col.gameObject);
        //}


    }

    private void Update()
    {

        if (GotWeapon == false && Timer.value <= 10)
        {
            agent.SetDestination(QueuePos.transform.position);

        }


        if (GotWeapon == false && (Timer.value >= 10 || leaving))
        {
            Flee();
        }





        if (GotWeapon == true)
        {
            BacktoBattle();

        }

    }

    public void Flee()
    {
        agent.SetDestination(fleePos.transform.position);
        Bubble.enabled = false;
        Debug.Log("Leaves");



    }


    public void BacktoBattle()
    {
        Debug.Log("aaaaaaaaaaaaaah!");
        Bubble.enabled = false;

        agent.SetDestination(battlePos.transform.position);



        ;
    }
}
