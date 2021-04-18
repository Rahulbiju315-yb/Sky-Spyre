using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float animationSpeed = 1f;

    [SerializeField] float runSpeed = 5f; //Left Facing Velocity
    public bool isWizard = false;

    Rigidbody2D enemyRigidBody;
    Animator enemyAnimator; // Reference to animator for dying animation

    GameObject patrolGround;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        CorrectAnimSpeed();

        if (!isWizard)
            EnemyMovement();
    }

    private void CorrectAnimSpeed()
    {

            animationSpeed= 1f / Time.timeScale;



        enemyAnimator.SetFloat("WizSpeed", animationSpeed);

    }

    public void Dying()
    {
        AudioManager manager = FindObjectOfType<AudioManager>();
        if (manager != null)
        {
            if (!isWizard)
            {

                manager.Play("Worm Death");
            }
            if (isWizard)
            {

                 manager.Play("Wizard Death");
            }
        }
        enemyAnimator.SetTrigger("Die");

        GetComponent<CapsuleCollider2D>().enabled = false;  // To prevent any collisions after the enemy is dead
        GetComponent<BoxCollider2D>().enabled = false;
        enemyRigidBody.bodyType = RigidbodyType2D.Static; // To prevent the enemy from moving after they are dead

        StartCoroutine(DestroyEnemy()); 


    }

    IEnumerator DestroyEnemy() // Coroutine to make enemy disappear after 2s
    {

        yield return new WaitForSecondsRealtime(1);

        Destroy(gameObject);
    }
    private void EnemyMovement()
    {
        enemyTimeBubble();

        if (isFacingLeft())
        {
            enemyRigidBody.velocity = new Vector2(-runSpeed, 0f);
        }
        else
        {
            enemyRigidBody.velocity = new Vector2(runSpeed, 0);
        }
    }

    private void enemyTimeBubble()
    {
            runSpeed = 5f / Time.timeScale;


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (patrolGround == null)
            patrolGround = collision.GetComponent<Collider2D>().gameObject;
        
        if(collision.GetComponent<Collider2D>().gameObject == patrolGround)
            flipSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (patrolGround == null)
            patrolGround = collision.GetComponent<Collider2D>().gameObject;

        if (collision.GetComponent<Collider2D>().gameObject != patrolGround)
            flipSprite();
    }
    private void flipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(enemyRigidBody.velocity.x), 1f); // Check the sign of the velocity
    }

    private bool isFacingLeft()
    {
        return transform.localScale.x > 0;       // Original (Scale 1,1,1) is facing left
    }
}
