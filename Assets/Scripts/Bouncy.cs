using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour
{
    // Start is called before the first frame update
    public float bounceSpeed = 50f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Player")
        {
            collision.collider.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bounceSpeed);
        }
    }
}
