using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Gravity : MonoBehaviour
{
    [SerializeField] float animationSpeed  =1f;

    [SerializeField] Vector2 explosionForce = new Vector2(100f, 100f);
    [SerializeField] float bombRadius = 3f;
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        CorrectAnimSpeed();
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


        myAnimator.SetFloat("BombSpeed", animationSpeed);

    }

    // We deleted the Update() method
    void ExplodeBomb()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, bombRadius, LayerMask.GetMask("Player"));   // Check if the player is in the vicinity of the bomb
    
        // Arguments are Center co-ord, radius, layer that we want it to interact with
    
        if(playerCollider) // To check if player is inisde the bombRadius
        {
            // 1. Push the Player in the right direction

            Vector2 correctDirection = new Vector2(-playerCollider.GetComponent<Player>().transform.localScale.x, 1f);
            playerCollider.GetComponent<Rigidbody2D>().AddForce(explosionForce * correctDirection);
            
            //2. Give the player velocity and make them unable to move for 2s afterwards. (We used this for getting hit by the pig(Enemy) too)
            
            playerCollider.GetComponent<Player>().PlayerHit();


        }




    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator.SetTrigger("Burn");     // To trigger the burning animation
    }

    void DestroyBomb()
    {
        Destroy(gameObject);  // Removes a GameObject. Used in event after the Booom animation
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, bombRadius);
    }
}
