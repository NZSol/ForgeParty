using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NpcRequest : MonoBehaviour
{
    public Canvas Bubble;
    public Slider Timer;
    public bool GotWeapon;
    public GameObject Battlefield;




    private void Start()
    {
        Bubble.enabled = false;
    }

    private void OnTriggerEnter(Collider other) //Trigger Zone Near benches
    {
        GotWeapon = false;

       
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
        gameObject.transform.position = Battlefield.transform.position;

    }
}
