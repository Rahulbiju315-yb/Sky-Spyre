using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput; // Important

public class Hazel_Aadit : MonoBehaviour
{

    [SerializeField] float runSpeed = 10f;  //Similar to a private variable
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 8f;
    [SerializeField] Vector2 hitSpeed = new Vector2(50f, 50f);
    [SerializeField] Transform hurtBox;  // For Attacking
    [SerializeField] float attackRadius = 3f;

    private Rigidbody2D myRigidBody2D;
    private Animator myAnimator;
    private BoxCollider2D myBoxCollider2D;
    PolygonCollider2D myPlayerFeet;

    public TimeManager timeManager;

    public float hangtime = 0.2f;
    private float hangCounter;

    public float jumpBufferLength = 0.1f;
    private float jumpBufferCount;

    public float horizontalDamping = 0.22f;

    public ParticleSystem dust;


    public bool isHit = false; // To prevent player from being able to move for 2s after they are hit

    float startingGravityScale;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>(); // We need a reference to our player's rigid body (For HORIZONTAL MOVEMENT)
        myAnimator = GetComponent<Animator>(); // We need a reference to our player's animator (FOR CHANGING ANIMATIONS)
        myBoxCollider2D = GetComponent<BoxCollider2D>();// We need a reference to our player's box collider 2d (FOR PREVENTING MULTI JUMPS)
        myPlayerFeet = GetComponent<PolygonCollider2D>(); // We need a reference to our player's polygon collider 2d (FOR PREVENTING WALL JUMPS)

        startingGravityScale = myRigidBody2D.gravityScale; // We need a reference to our player's initial gravity scale (FOR STICKING TO THE DRAPES WHILE CLIMBING)


        myAnimator.SetTrigger("Appearing");

    }

    // Update is called once per frame
    void Update()
    {

        if (!isHit) // Player can only do something if they have not been hit
        {


            Run();
            Jump();
            Climb();
            Attack();

            if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy"))) // If player touches, the enemy, they are thrown back (with special animation) and can't move for 2 seconds
            {
                PlayerHit();

            }

            ExitLevel();

        }

        if(timeManager!=null)
            timeManager.DoSlowMotion(); // TIME!!!

       

    }

    private void ExitLevel()
    {
        if (!myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Interactable")))
            return;

        if (CrossPlatformInputManager.GetButtonDown("Vertical"))
        {
            myAnimator.SetTrigger("Disappearing");


        }
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        FindObjectOfType<ExitThisDoor>().StartLoadingNextLevel();
        TurnOffRenderer();

    }

    public void TurnOffRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Attack()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1")) // Check the button strings in Project Settings->Input Manager
        {
            if (FindObjectOfType<AudioManager>() != null)
                FindObjectOfType<AudioManager>().Play("Attack");

            myAnimator.SetTrigger("Attacking"); // Activate the animation

            Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(hurtBox.position, attackRadius, LayerMask.GetMask("Enemy")); // OverlapCircleAll returns an array of Collider2Ds

            foreach (Collider2D enemy in enemiesToHit)
            {
                enemy.GetComponent<Enemy>().Dying(); // Get a reference to the script of a collider in the array and call the method Dying()
            }


        }



    }

    public void PlayerHit()
    {
        FindObjectOfType<AudioManager>().Play("Hit");

        myRigidBody2D.velocity = hitSpeed * new Vector2(-transform.localScale.x, 1f); // -transform.localScale.x (-1 or 1) kicks the  player AWAY from the enemy
       

        myAnimator.SetTrigger("Hitting");

        isHit = true; // Prevents player from doing anyting for 2s

        FindObjectOfType<GameSession>().processPlayerDeath(); // Subtracts 1 life if lives (before subtracting) > 1 and resets game, otherwise

        StartCoroutine(stopBeingHit()); // Coroutine suspends the main program until a condition is met (Condition is : player being able to move again after 2s)

    }

    IEnumerator stopBeingHit() // Coroutine that prevents player from moving after being hit
    {

        yield return new WaitForSecondsRealtime(1f); // yield is used for the 'Suspension' aspect of this

        isHit = false;
    }

    private void Climb() // More Similar to running than jumping
    {
        if (myPlayerFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))) // Do this only if the player touches the 'Climbing' layer
        {

            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");

            Vector2 climbingVelocity = new Vector2(0, controlThrow * climbSpeed); // Only Change the Y direction, Make X 0 to kill any horizontal veloccity when player jumps onto the Drapes

            myRigidBody2D.velocity = climbingVelocity;

            myAnimator.SetBool("Climbing", true); // Whenever player touches the drapes, activate Climbing animation

            myRigidBody2D.gravityScale = 0f; // Makes player stick to the drapes
        }
        else
        {
            myAnimator.SetBool("Climbing", false); // Else, go back to Idling animation
            myRigidBody2D.gravityScale = startingGravityScale; // Go back to normal gravity when not touching the drapes
        }


    }

    private void Jump()
    {


        Boolean isGrounded = myPlayerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));

        Boolean isJumping = CrossPlatformInputManager.GetButtonDown("Jump");

        if(isGrounded)                                      // Modification 2 Hang Time (Jump a little late, after leaving the platform)
        {

            hangCounter = hangtime;
        }
        else
        {
            hangCounter -= Time.unscaledDeltaTime;
        }

        if(isJumping)                                       // Modification 3 Jump Buffer (Jump a little early, before hitting ground)
        {
            jumpBufferCount = jumpBufferLength;
        }
        else
        {
            jumpBufferCount -= Time.unscaledDeltaTime;
        }



        if (jumpBufferCount>=0 && hangCounter>0f)
        {   
            CreateDust();
            myAnimator.SetTrigger("J");
            if(FindObjectOfType<AudioManager>()!=null)
                FindObjectOfType<AudioManager>().Play("Jump");

            Vector2 jumpVelocity = new Vector2(myRigidBody2D.velocity.x, jumpSpeed); // If the user presses space (default positive button for "Jump", player gets an impulse in +Y direction)
            myRigidBody2D.velocity = jumpVelocity;

            jumpBufferCount = 0;
        }

        if(CrossPlatformInputManager.GetButtonUp("Jump")&&myRigidBody2D.velocity.y>0)
        {
            CreateDust();

            //FindObjectOfType<AudioManager>().Play("Jump");
            myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, myRigidBody2D.velocity.y*0.5f);  // Modification 1 Mini tap jumps
        }



    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // Needs a string argument from Project Settings->Input Manager->Axes->Horizontal

        float horizontalVelocity = controlThrow * runSpeed;
        horizontalVelocity *= Mathf.Pow(1f-horizontalDamping, Time.unscaledDeltaTime*10f);
        Vector2 playerVelocity = new Vector2(horizontalVelocity, myRigidBody2D.velocity.y); // Y velocity doesn't change

        myRigidBody2D.velocity = playerVelocity;


        flipSprite();
        toRunningState();

    }

    private void toRunningState()
    {
        bool isRunning = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        if(isRunning)
        {
            CreateDust();


        }
        myAnimator.SetBool("Running", isRunning);
    }

    private void flipSprite()
    {
        // Mathf.Abs returns the absolute value
        //Mathf.Sign returns +-1 for positive/negative numbers
        //Mathf.Epsilon is a very small positive number 0.000001


        bool runningHorizontally = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;    //Recall when we did the run originally, controlThrow went very close to zero, but never exactly zero
                                                                                           //Hence, we compare with Mathf.Epsilon, instead of 0 directly

        if (runningHorizontally) // Apply only to moving objects
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x), 1f);   // If vx>0, X scale is 1, if vx<0, X scale is -1 (flipped)

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hurtBox.position, attackRadius);  // To visualise the attacking radius
    }

    void CreateDust()
    {
        dust.Play();
    }
}


