using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class Movement : MonoBehaviour
{
    public float speed;

    Rigidbody rb;
    GameObject Player;

    public float maxSpeed = 5;
    public float minSpeed = -5;
    public float dashForce = 5;
    public float dashCool = 0;

    public float damping;
    Vector2 lookDir;

    bool inputArmed = true;
    bool count;


    public Slider slide;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
        rb = Player.GetComponent<Rigidbody>();
        text.enabled = false;
        Player.transform.position = new Vector3(0, 1.2f, 7.3f);
    }

    private void OnEnable()
    {
    }

    public void Move(CallbackContext context)
    {
        var stick = context.ReadValue<Vector2>();


        moveValues = new Vector3(stick.x, 0, stick.y);
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
            maxSpeed = 10;
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
        maxSpeed = Mathf.Lerp(5, 10, dashing);

        if (dashCool > 0)
        {
            dashCool -= Time.deltaTime;
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