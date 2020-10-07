using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerbump : MonoBehaviour
{
    public Transform Bump;
    public Transform swearwords;
    public Transform flames;
    public Transform smoke;
    public Transform sparks;
    public Transform cannon;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Bump.GetComponent<ParticleSystem>().Play();
        }

        if (Input.GetKeyDown("c"))
        {
            swearwords.GetComponent<ParticleSystem>().Play();
        }

        if (Input.GetKeyDown("v"))
        {
            flames.GetComponent<ParticleSystem>().Play();
        }

        if (Input.GetKeyDown("b"))
        {
            smoke.GetComponent<ParticleSystem>().Play();
        }

        if (Input.GetKeyDown("n"))
        {
            sparks.GetComponent<ParticleSystem>().Play();
        }

        if (Input.GetKeyDown("m"))
        {
            cannon.GetComponent<ParticleSystem>().Play();
        }
    } 

}
