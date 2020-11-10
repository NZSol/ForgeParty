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
    public float dashForce = 0;
    public float dashCool = 0;

    float startMaxSpeed = 0;

    public Vector2 stick = Vector2.zero;

    Vector2 lookDir = Vector2.zero;

    bool inputArmed = true;
    bool canMove = true;
    [SerializeField] float moveTimer = 0;
    [SerializeField] float push = 0;
    
    float dashing = 0;
    float haltTime = 0;
    float damping = 0;

    Vector3 storedVel = new Vector3();
    Vector3 moveValues = new Vector3();
    Vector3 midPoint = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
        rb = Player.GetComponent<Rigidbody>();
        startMaxSpeed = maxSpeed;

        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
    }


    public void Move(CallbackContext context)
    {
        moveValues = new Vector3(stick.x, 0, stick.y) * 6;
        lookDir = stick;
    }


    public void Dash(CallbackContext context)
    {
        if (context.started && inputArmed && dashCool <= 0)
        {
            inputArmed = false;
            dashing = 1;
            dashCool = 3;
        }
        if (context.canceled)
        {
            inputArmed = true;
        }
    }
    
    
    void Update()
    {
        MoveNow();

        if (dashing > 0)
        {
            dashing -= Time.deltaTime * 2;
        }
        else
        {
            dashing = 0;
        }

        if (dashCool > 0)
        {
            dashCool -= Time.deltaTime / 2;
        }
    }
    
    void MoveNow()
    {

        if (moveValues.magnitude != 0)
        {
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(lookDir.x, lookDir.y) * 180 / Mathf.PI, 0);
        }

        if (moveValues.magnitude > 1)
        {
            moveValues = moveValues.normalized;
        }


        if (stick != Vector2.zero && canMove)
        {
            haltTime = 0;
            rb.velocity = moveValues * Mathf.Lerp(speed, dashForce, dashing) + storedVel;
            damping = Mathf.Clamp(damping, 1, 3);
            damping += Time.deltaTime;
        }
        else
        {
            haltTime += Time.deltaTime / ((int)damping/2);
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, haltTime);
        }
        storedVel = rb.velocity * 0.1f;


        //Clamp RigidBody Velocity
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canMove = false;
            midPoint = (transform.position + collision.gameObject.transform.position) / 2;
            StartCoroutine(ResumeMove());
            StartCoroutine(AddRBForce());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        speed = 35;
    }

    IEnumerator ResumeMove()
    {
        yield return new WaitForSeconds(moveTimer);
        canMove = true;
    }

    IEnumerator AddRBForce()
    {
        yield return new WaitForFixedUpdate();
        rb.AddForce((transform.position - midPoint) * (rb.velocity.magnitude / 2), ForceMode.Impulse);
    }
}