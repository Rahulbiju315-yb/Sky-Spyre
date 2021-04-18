using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] Vector2 explosionForce = new Vector2(100f, 100f);
    [SerializeField] float bombRadius = 3f;
    [SerializeField] float animationSpeed = 1.5f;
    Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
    void Update()
    {

        CorrectAnimSpeed();
    }

    private void CorrectAnimSpeed()
    {
            animationSpeed = 1.5f / Time.timeScale;


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

            Vector2 correctDirection = new Vector2(-playerCollider.GetComponent<Hazel_Aadit>().transform.localScale.x, 1f);
            playerCollider.GetComponent<Rigidbody2D>().AddForce(explosionForce * correctDirection);
            
            //2. Give the player velocity and make them unable to move for 2s afterwards. (We used this for getting hit by the pig(Enemy) too)
            
            playerCollider.GetComponent<Hazel_Aadit>().PlayerHit();


        }




    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator.SetTrigger("Burn");     // To trigger the burning animation

        FindObjectOfType<AudioManager>().Play("Bomb");
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
