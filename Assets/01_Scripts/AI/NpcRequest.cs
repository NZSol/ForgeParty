using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NpcRequest : MonoBehaviour
{
    public Canvas Bubble;
    public Slider Timer;
    public bool GotWeapon;
    public Transform Battlefield;
    public Transform ForgeRequestZone;
    public float speed = 1f;
    public float startTime;
   private float LengthJourney;




    private void Start()
    {
        Bubble.enabled = false;
        startTime = Time.time;
        GotWeapon = false;
        LengthJourney = Vector3.Distance(ForgeRequestZone.position, Battlefield.position);
       
     

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
            GotWeapon = false;

            Bubble.enabled = true;
        }
    }

    private void Update()
    {
        if (GotWeapon == false && Timer.value <=10)
        {
            float distCovered = (Time.time - startTime) * speed;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, ForgeRequestZone.position, (distCovered / LengthJourney));
        }

  
        if (GotWeapon == false && Timer.value >= 10)
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
        Bubble.enabled = false;  
        Debug.Log("Leaves");
       Destroy(gameObject);
        
    }


    public void BacktoBattle()
    {
        Debug.Log("aaaaaaaaaaaaaah!");
        Bubble.enabled = false;
        float distCovered = (Time.time - startTime) * speed;
        gameObject.transform.position = Vector3.Lerp(ForgeRequestZone.position, Battlefield.position, (distCovered / LengthJourney));
      ;
    }
}
