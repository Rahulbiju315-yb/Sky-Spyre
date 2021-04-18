using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput; // Important
using UnityEngine.SceneManagement; 
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;  //Similar to a private variable
    [SerializeField] public float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 8f;
    [SerializeField] Vector2 hitSpeed = new Vector2(50f, 50f);
    [SerializeField] Transform hurtBox;  // For Attacking
    [SerializeField] float attackRadius = 3f;

    private Rigidbody2D myRigidBody2D;
    private Animator myAnimator;
    private CapsuleCollider2D capsuleCollider;
    PolygonCollider2D myPlayerFeet;

    bool isHit = false; // To prevent player from being able to move for 2s after they are hit

    float startingGravityScale;

    public static int maxChkpPriority;
    static Vector3 chkpPosition;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>(); // We need a reference to our player's rigid body (For HORIZONTAL MOVEMENT)
        myAnimator = GetComponent<Animator>(); // We need a reference to our player's animator (FOR CHANGING ANIMATIONS)
        capsuleCollider = GetComponent<CapsuleCollider2D>();// We need a reference to our player's box collider 2d (FOR PREVENTING MULTI JUMPS)
        myPlayerFeet = GetComponent<PolygonCollider2D>(); // We need a reference to our player's polygon collider 2d (FOR PREVENTING WALL JUMPS)

        startingGravityScale = myRigidBody2D.gravityScale; // We need a reference to our player's initial gravity scale (FOR STICKING TO THE DRAPES WHILE CLIMBING)

        myAnimator.SetTrigger("Appearing");

        if (SceneManager.GetActiveScene().name == "Dark")
        {
            if (maxChkpPriority == 0)
                chkpPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            else
            {
                transform.position = new Vector3(chkpPosition.x, chkpPosition.y, chkpPosition.z);
            }
        }
    
    }

    // Update is called once per frame
    void Update()
    {

        if(!isHit) // Player can only do something if they have not been hit
        {
            
        Run();
        Jump();
        Climb();
        Attack();

        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))) // If player touches, the enemy, they are thrown back (with special animation) and can't move for 2 seconds
        {
            PlayerHit();

        }

            ExitLevel();

        }
        
        
    }

    private void ExitLevel()
    {
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Interactable")))
            return;

        if(CrossPlatformInputManager.GetButtonDown("Vertical"))
        {
            myAnimator.SetTrigger("Disappearing");
            

        }
    }

    public void LoadNextLevel()
    {
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
            myAnimator.SetTrigger("Attacking"); // Activate the animation

            Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(hurtBox.position, attackRadius, LayerMask.GetMask("Enemy")); // OverlapCircleAll returns an array of Collider2Ds
            Collider2D[] bossHit = Physics2D.OverlapCircleAll(hurtBox.position, attackRadius, LayerMask.GetMask("Boss"));

            foreach(Collider2D enemy in enemiesToHit)
            {
                Enemy e = enemy.GetComponent<Enemy>();
                if (e != null)
                { 
                    e.Dying(); // Get a reference to the script of a collider in the array and call the method Dying()
                }
            }

            foreach(Collider2D boss in bossHit){
                DarkWizard d = boss.GetComponent<DarkWizard>();
                if(d!=null){
                    d.Hit();
                }
            }


        }



    }

    public void PlayerHit()
    {

        myRigidBody2D.velocity = hitSpeed * new Vector2(-transform.localScale.x, 1f); // -transform.localScale.x (-1 or 1) kicks the  player AWAY from the enemy
        myAnimator.SetTrigger("Hitting");

        isHit = true; // Prevents player from doing anyting for 2s

        FindObjectOfType<GameSession>().processPlayerDeath(); // Subtracts 1 life if lives (before subtracting) > 1 and resets game, otherwise

        StartCoroutine(stopBeingHit(2f)); // Coroutine suspends the main program until a condition is met (Condition is : player being able to move again after 2s)

    }

    IEnumerator stopBeingHit(float f) // Coroutine that prevents player from moving after being hit
    {

        yield return new WaitForSeconds(f); // yield is used for the 'Suspension' aspect of this

        isHit = false;
    }

    private void Climb() // More Similar to running than jumping
    {
        if (myPlayerFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))) // Do this only if the player touches the 'Climbing' layer
        {
        
            float controlThrowV = CrossPlatformInputManager.GetAxis("Vertical");
            float controlThrowH = CrossPlatformInputManager.GetAxis("Horizontal");

            Vector2 climbingVelocity = new Vector2(controlThrowH * climbSpeed, controlThrowV * climbSpeed);

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

        //To prevent wall jumping:
        if(!myPlayerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) // If the Player isn't touching the ground, they can't jump again
        {
            return;
        }


        Boolean isJumping = CrossPlatformInputManager.GetButtonDown("Jump");

        if(isJumping)
        {
            Vector2 jumpVelocity = new Vector2(myRigidBody2D.velocity.x, jumpSpeed); // If the user presses space (default positive button for "Jump", player gets an impulse in +Y direction)
            myRigidBody2D.velocity = jumpVelocity;
        }

    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // Needs a string argument from Project Settings->Input Manager->Axes->Horizontal

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2D.velocity.y); // Y velocity doesn't change

        myRigidBody2D.velocity = playerVelocity;


        flipSprite();
        toRunningState();

    }

    private void toRunningState()
    {
        bool isRunning = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer ==  LayerMask.NameToLayer("Traps"))
        {
            OnTrapHit();
        }
    }

    private void OnTrapHit()
    {
        myAnimator.SetTrigger("Hitting");
        isHit = true;
        StartCoroutine(stopBeingHit(2f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (maxChkpPriority > 0 && SceneManager.GetActiveScene().name == "Dark")
        {
            transform.position = new Vector3(chkpPosition.x, chkpPosition.y, transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("Traps"))
        {
            OnTrapHit();
        }

        if (collision.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("SavePoint"))
        {
            int priority = collision.GetComponent<Collider2D>().gameObject.GetComponent<SavePoint>().priority;
            if (priority > maxChkpPriority)
            {
                maxChkpPriority = priority;
                chkpPosition.x = transform.position.x;
                chkpPosition.y = transform.position.y;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hurtBox.position, attackRadius);  // To visualise the attacking radius
    }
}