using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PickupHeart : MonoBehaviour
{

    [SerializeField] int heartLives = 1; // No. of lives a player gets when they collect a heart

    [SerializeField] float animationSpeed = 1f;
    Animator heartAnimator;

    void Start()
    {
        heartAnimator = GetComponent<Animator>();
    }
    void Update()
    {

        CorrectAnimSpeed();
    }

    private void CorrectAnimSpeed()
    {

        if (Time.timeScale < 1f)
            animationSpeed = 1 / Time.timeScale;

        if (Time.timeScale >= 1f)
        {
            animationSpeed = 1f;
        }


        heartAnimator.SetFloat("HeartSpeed", animationSpeed);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<AudioManager>().Play("Heart");

        FindObjectOfType<GameSession>().AddLife(heartLives);
        
        Destroy(gameObject); // Make the heart disappear, when player touches them
    }
}
