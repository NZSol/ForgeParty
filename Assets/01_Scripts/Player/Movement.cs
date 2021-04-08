using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class Movement : MonoBehaviour
{
    public float speed = 0;
    float updateSpeed = 0;

    Rigidbody rb = null;
    GameObject Player = null;


    public float maxSpeed = 0;
    public float dashForce = 0;
    public float dashCool = 0;
    public float dashCoolMax = 3;
    public float bompForce = 0;
    public float bompFalloff = 0;
    public float bompLimit = 0;
    
    [SerializeField]
    GameObject staminaMeter = null;
    [SerializeField]
    Image staminaImg = null;

    public Vector2 stick = Vector2.zero;

    Vector2 lookDir = Vector2.zero;

    bool inputArmed = true;
    bool canMove = true;
    bool addForce = false;
    [SerializeField] float moveTimer = 0;
    [SerializeField] float push = 0;
    
    public float dashing = 0;
    float haltTime = 0;
    float damping = 0;

    Vector3 storedVel = new Vector3();
    Vector3 moveValues = new Vector3();
    Vector3 force = new Vector3();

    Animator anim = null;
    float blendVal = 0;


    // Start is called before the first frame update
    void Start()
    {
        dashCool = dashCoolMax;
        Player = this.gameObject;
        rb = Player.GetComponent<Rigidbody>();
        updateSpeed = speed;

        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
        anim = gameObject.GetComponent<Animator>();
    }


    public void Move(CallbackContext context)
    {
        moveValues = new Vector3(stick.x, 0, stick.y) * 6;
        lookDir = stick;
    }


    public void Dash(CallbackContext context)
    {
        if (context.started && inputArmed && dashCool >= dashCoolMax)
        {
            inputArmed = false;
            dashing = 1;
            dashCool = 0;
            blendVal = 3;
        }
        if (context.canceled)
        {
            inputArmed = true;
        }
    }

    void Update()
    {
        if (dashing > 0)
        {
            dashing -= Time.deltaTime * 2;
        }
        else
        {
            dashing = 0;
        }

        if (dashCool < dashCoolMax)
        {
            dashCool += Time.deltaTime / 2;
            if (!staminaMeter.activeSelf)
            {
                staminaMeter.gameObject.SetActive(true);
            }
        }
        else
        {
            staminaMeter.gameObject.SetActive(false);
        }
        var timer = dashCool / dashCoolMax;
        staminaImg.fillAmount = timer;
        staminaImg.color = Color.Lerp(Color.red, Color.green, timer);




        anim.SetFloat("Blend", blendVal);
    }

    private void FixedUpdate()
    {
        MoveNow();
        AddRBForce();
        force *= bompFalloff;
        canMove = force.magnitude < bompLimit;
    }

    void MoveNow()
    {
        if (stick == Vector2.zero)
        {
            anim.SetBool("Move", false);
        }
        else
        {
            anim.SetBool("Move", true);
        }
        if (anim.GetBool("Anvil") || anim.GetBool("Bellows"))
        {
            stick = Vector2.zero;
        }

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
            if (blendVal > 2)
            {
                blendVal -= Time.deltaTime;
            }
            else
            {
                blendVal = 2;
            }
        }
        else
        {
            haltTime += Time.deltaTime / ((int)damping/2);
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, haltTime);

            blendVal -= Time.deltaTime * ((int)damping * 4);
            blendVal = Mathf.Clamp(blendVal, 0, 3);
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
            var midPoint = (transform.position + collision.gameObject.transform.position) / 2;
            force = (transform.position - midPoint).normalized * bompForce;
            addForce = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        speed = updateSpeed;
    }

    void AddRBForce()
    {
        rb.velocity += force;
    }
}