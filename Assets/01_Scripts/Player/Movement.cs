﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] string Vert;
    [SerializeField] string Horiz;
    public float speed;

    Rigidbody rb;
    GameObject Player;
    CharacterController Char;

    public float maxSpeed = 5;
    public float minSpeed = -5;

    Vector3 dir = new Vector3(0,0,0);
    float rotSpeed = 3;
    public float damping;
    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
        rb = Player.GetComponent<Rigidbody>();
        Char = Player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }



    public bool control;
    Vector3 moveValues;

    IEnumerator Move()
    {
        float VertInput = Input.GetAxis(Vert);
        float HorizInput = Input.GetAxis(Horiz);
        //Vector3 moveValues = new Vector3(HorizInput, 0, VertInput);
        //float moveMag = Vector3.Magnitude(moveValues);

        if(control == true)
        {
            moveValues = new Vector3(0, Mathf.Atan2(HorizInput, VertInput) * 180 / Mathf.PI, 0);
        }
        else
        {
            moveValues = new Vector3(HorizInput, 0, VertInput);
        }



        if (rb.velocity.magnitude != 0)
        {
            //transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxis(Horiz), Input.GetAxis(Vert)) * 180 / Mathf.PI, 0);
        }

        rb.velocity += moveValues;

        ////Clamp RigidBody Velocity
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        else if (rb.velocity.magnitude < minSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, minSpeed);
        }


        yield return null;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Collider>().material != null)
        {
            rb.velocity = -rb.velocity;
            print(collision.gameObject);
            print(collision.gameObject.GetComponent<Collider>().material);

        }
    }

}

/*

    IEnumerator Move()
    {
        yield return new WaitForFixedUpdate();

        float VertInput = Input.GetAxis(Vert);
        float HorizInput = Input.GetAxis(Horiz);
        Vector3 moveValues = new Vector3(HorizInput, 0, VertInput) * speed;

        Char.SimpleMove(moveValues);

    }

}
*/
