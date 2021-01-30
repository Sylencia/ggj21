using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
//using DG.Tweening;        remember to use DOTween

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 moveInput;

    private GameObject mop;

    private Camera mainCam;

    private bool grounded;
    private float lastJump;

    [SerializeField] //makes private variables show up in Inspector
    private float currentSpeed;

    public float moveSpeed = 12;
    public float maxMoveSpeed = 20;
    public float jumpForce = 5;

    private GameManager gm;


    // Start is called before the first frame update
    //if necessary use Awake() to execute things (on 'this' script) before the first Start, which is more for linking with other components
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        mop = GameObject.FindGameObjectWithTag("Mop");

        mainCam = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        //count the time since the last jump
        lastJump += Time.deltaTime;
    }

    void FixedUpdate()
    {
        //FIRST ROTATE PLAYER RIGIDBODY TO MATCH CAMERA
        Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, mainCam.transform.eulerAngles.y, transform.eulerAngles.z);
        rb.rotation = Quaternion.Euler(eulerRotation);

        //THEN MOVE PLAYER RELATIVE TO ROTATION
        rb.AddRelativeForce(moveInput * moveSpeed);
        
        currentSpeed = rb.velocity.magnitude;      // get the latest speed

        capMaxSpeed();


        //FASTER FALLING, GIVES LESS FLOATY FEEL BY INCREASING GRAVITY WHEN AIRBORNE
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * 2.5f * Time.deltaTime;
        }
    }

    public void capMaxSpeed()
    {
        if (currentSpeed > maxMoveSpeed)     //max speed should only apply to player iniated actions, not gravity (not physics accurate but it works well)
        {
            float brakeSpeed = currentSpeed - maxMoveSpeed;  // calculate the speed decrease

            Vector3 normalisedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = -normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            rb.AddForce(brakeVelocity, ForceMode.Impulse);  // apply opposing brake force
        }
    }


    /* RESPONDING TO INPUT */

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (gm.IsGameRunning())
        {
            Vector2 rawInput = ctx.ReadValue<Vector2>();
            moveInput.x = rawInput.x;
            moveInput.z = rawInput.y;
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (grounded && gm.IsGameRunning())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            lastJump = 0.0f;    //reset jump timer
        }
    }

    public void OnMop(InputAction.CallbackContext ctx)
    {
        if (gm.IsGameRunning())
        {
            //DoSomething
            Animator anim = mop.GetComponent<Animator>();
            anim.Play("Swing");
        }
    }

    public void OnSweep(InputAction.CallbackContext ctx)
    {
        if (gm.IsGameRunning())
        {
            //DoSomething
            Animator anim = mop.GetComponent<Animator>();
            anim.Play("Sweep");
        }
    }

    public void OnGrab(InputAction.CallbackContext ctx)
    {
        if (gm.IsGameRunning())
        {
            //DoSomething
            
        }
    }

    /* TRIGGERS AND COLLIDERS */

    void OnCollisionEnter(Collision other)   //void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Floor"))
        //{
           //
        //}
    }

    void OnCollisionStay(Collision other)
    {
        // this allows jumping while touching any object other object
        grounded = true;
    }

    void OnCollisionExit(Collision other)     //void OnTriggerExit(Collider other)
    {
        grounded = false;
    }

    /* COROUTINES */

    IEnumerator pauseForTime(float duration)
    {
        Time.timeScale = 0.01f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1.0f;
    }
}

