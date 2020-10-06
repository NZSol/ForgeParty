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
    public Transform Battlefield;
    public Transform ForgeRequestZone;
    public Transform OutOfBattle;
    public float speed = 1f;
    public float startTime;
    NavMeshAgent agent;

    private bool leaving;
    



    private void Start()
    {
       agent=  GetComponent<NavMeshAgent>();

        


        Bubble.enabled = false;
        startTime = Time.time;
        GotWeapon = false;
       
       
     

    }

    private void OnTriggerEnter(Collider other) //Trigger Zone Near benches
    {
       

       
       if (other.tag == "Sword")
       {
            GotWeapon = true;
            Destroy(other.gameObject);
            
        }

        if (other.tag == "requestZone")
        {
           

            Bubble.enabled = true;
        }
    }

    private void Update()
    {
        if (GotWeapon == false && Timer.value <=10)
        {
            agent.destination = ForgeRequestZone.position;
           
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
        agent.destination = OutOfBattle.position;
        Bubble.enabled = false;
        leaving = true;
        Debug.Log("Leaves");


       
    }


    public void BacktoBattle()
    {
        Debug.Log("aaaaaaaaaaaaaah!");
        Bubble.enabled = false;

        agent.destination = Battlefield.position;



    ;
    }
}
