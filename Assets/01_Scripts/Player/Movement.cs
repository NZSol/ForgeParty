using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class Movement : MonoBehaviour
{
    public float speed = 0;

    Rigidbody rb = null;
    GameObject Player = null;

    public float maxSpeed = 0;
    public float minSpeed = 0;
    public float dashForce = 0;
    public float dashCool = 0;

    float startMaxSpeed = 0;
    float startMinSpeed = 0;

    public Vector2 stick = Vector2.zero;

    public float damping = 0;
    Vector2 lookDir = Vector2.zero;

    bool inputArmed = true;
    bool count = false;


    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
        rb = Player.GetComponent<Rigidbody>();
        startMaxSpeed = maxSpeed;
        startMinSpeed = minSpeed;
    }


    public void Move(CallbackContext context)
    {
        moveValues = new Vector3(stick.x, 0, stick.y) * 6;
        lookDir = stick;
    }


    float dashing = 0;
    public void Dash(CallbackContext context)
    {
        if (context.started && inputArmed && dashCool <= 0)
        {
            inputArmed = false;
            dashing = 1;
            DoDash();
        }
        if (context.canceled)
        {
            inputArmed = true;
        }
    }



    void DoDash()
    {
        if (rb != null)
        {
            rb.velocity += moveValues * dashForce;
            maxSpeed = startMaxSpeed;
            dashCool = 5;
        }
    }
    
    
    void Update()
    {
        MoveNow();

        if (dashing > 0)
        {
            dashing -= Time.deltaTime;
        }
        else
        {
            dashing = 0;
        }
        maxSpeed = Mathf.Lerp(maxSpeed, dashForce, dashing);

        if (dashCool > 0)
        {
            dashCool -= Time.deltaTime / 2;
        }


    }

    
    Vector3 storedVel;
    Vector3 moveValues;
    
    void MoveNow()
    {

        if (moveValues.magnitude != 0)
        {
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(lookDir.x, lookDir.y) * 180 / Mathf.PI, 0);
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

        rb.velocity += (moveValues.normalized + storedVel) * (Time.deltaTime * speed);
    }

    private void FixedUpdate()
    {
        storedVel = moveValues * Time.deltaTime;
    }
}