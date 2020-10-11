//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.AI;


//public class NpcRequest : NPC
//{
//    public Canvas Bubble;
//    public Slider Timer;
//    public bool GotWeapon;
//    public float speed = 1f;
//    public float startTime;
//    NavMeshAgent agent;

//    private bool leaving;

//    Weapon.weaponType weapon;



//    private void Start()
//    {


//       agent=  GetComponent<NavMeshAgent>();

        


//        Bubble.enabled = false;
//        startTime = Time.time;
//        GotWeapon = false;
       
       
     

//    }

//    private void OnTriggerEnter(Collider col) //Trigger Zone Near benches
//    {
//       if (col.gameObject.GetComponent<Weapon>().myWeapon == Weapon.weaponType.Sword)
//        {
//            GotWeapon = true;
//            Destroy(col.gameObject);
//        }


//        if (col.tag == "requestZone")
//        {

//            Bubble.enabled = true;
//        }
//    }

//    private void Update()
//    {
//        if (GotWeapon == false && Timer.value <=10)
//        {
//            agent.destination = ForgeRequestZone.position;
           
//        }

  
//        if (GotWeapon == false && (Timer.value >= 10 || leaving))
//        {
            
//            Flee();
//        }

        



//        if (GotWeapon == true)
//        {
//            BacktoBattle();
          
//        }

//    }

//    public void Flee()
//    {
//        agent.destination = OutOfBattle.position;
//        Bubble.enabled = false;
//        leaving = true;
//        Debug.Log("Leaves");


       
//    }


//    public void BacktoBattle()
//    {
//        Debug.Log("aaaaaaaaaaaaaah!");
//        Bubble.enabled = false;

//        agent.destination = Battlefield.position;



//    ;
//    }
//}
