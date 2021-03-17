using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float runSpeed = 5f; //Left Facing Velocity

    Rigidbody2D enemyRigidBody;


    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFacingLeft())
        {
            enemyRigidBody.velocity = new Vector2(-runSpeed, 0f); 
        }
        else
        {
            enemyRigidBody.velocity = new Vector2(runSpeed, 0);
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
