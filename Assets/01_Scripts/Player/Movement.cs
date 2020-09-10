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
    }


    public void Move(CallbackContext context)
    {
        var stick = context.ReadValue<Vector2>();

        moveValues = new Vector3(stick.x, 0, stick.y);
        lookDir = stick;
    }

    public void InteractPress(CallbackContext context)
    {
        if (context.started && inputArmed)
        {
            inputArmed = false;
            text.enabled = true;
            Invoke("cancel", 0.1f);
        }
        if (context.canceled)
        {
            inputArmed = true;
        }
    }
    
    float dashing = 0;
    public void Dash(CallbackContext context)
    {
        if (context.started && inputArmed)
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


    void cancel()
    {
        text.enabled = false;
    }

    float timer = 0;
    public void InteractHold(CallbackContext context)
    {
        if (context.started)
        {
            count = true;
        }
        if (context.canceled)
        {
            count = false;
            timer = 0;
            slide.value = timer;
        }
    }


    void DoDash()
    {
        if (rb != null)
        {
            rb.velocity += moveValues * dashForce;
            maxSpeed = 10;
        }
    }
    
    
    void Update()
    {
        MoveNow();

        if (count)
        {
            timer += Time.deltaTime;
            slide.value = timer;
        }
        if (dashing > 0)
        {
            dashing -= Time.deltaTime;
        }
        else
        {
            dashing = 0;
        }
        maxSpeed = Mathf.Lerp(5, 10, dashing);
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

        storedVel = moveValues * Time.deltaTime;
        rb.velocity += (moveValues.normalized + storedVel) * (Time.deltaTime * speed);
    }
}