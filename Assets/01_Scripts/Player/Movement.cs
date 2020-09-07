using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] string Vert;
    [SerializeField] string Horiz;
    public float speed;

    Rigidbody rb;
    GameObject Player;

    public float maxSpeed = 5;
    public float minSpeed = -5;


    public float damping;

    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
        rb = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    Vector3 storedVel;
    Vector3 moveValues;
    private void FixedUpdate()
    {
        storedVel = moveValues * Time.deltaTime;
        rb.velocity += (moveValues.normalized + storedVel) * (Time.deltaTime * speed);
    }

    
    void Move()
    {
        float VertInput = Input.GetAxisRaw(Vert);
        float HorizInput = Input.GetAxisRaw(Horiz);
        moveValues = new Vector3(HorizInput, 0, VertInput);




        print(rb.velocity);

        if (moveValues.magnitude != 0)
        {
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxis(Horiz), Input.GetAxis(Vert)) * 180 / Mathf.PI, 0);
        }



                    //Clamp RigidBody Velocity
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        else if (rb.velocity.magnitude < minSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, minSpeed);
        }

    }
}