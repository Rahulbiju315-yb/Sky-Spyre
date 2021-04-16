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
        if (Time.timeScale == 0.5f) //0.5x
        {
            animationSpeed = 2f;
        }

        if (Time.timeScale == 0.75f)
        {
            animationSpeed = 1.33f;
        }

        if (Time.timeScale >= 1f)
        {
            animationSpeed = 1f;
        }


        enemyAnimator.SetFloat("WizSpeed", animationSpeed);

    }

    public void Dying()
    {

        enemyAnimator.SetTrigger("Die");

        GetComponent<CapsuleCollider2D>().enabled = false;  // To prevent any collisions after the enemy is dead
        GetComponent<BoxCollider2D>().enabled = false;
        enemyRigidBody.bodyType = RigidbodyType2D.Static; // To prevent the enemy from moving after they are dead

        StartCoroutine(DestroyEnemy()); 


    }

    IEnumerator DestroyEnemy() // Coroutine to make enemy disappear after 2s
    {

        yield return new WaitForSeconds(2);

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
        if (Time.timeScale == 0.5f) //0.5x
        {
            runSpeed = 10f;
        }

        if (Time.timeScale == 0.75f)
        {
            runSpeed = 6.67f;
        }

        if (Time.timeScale >= 1f)
        {
            runSpeed = 5f;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
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
