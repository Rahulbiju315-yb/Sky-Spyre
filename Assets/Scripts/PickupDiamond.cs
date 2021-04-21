using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDiamond : MonoBehaviour
{
    [SerializeField] int diamondPoints = 2;


    [SerializeField] AudioClip diamondPickupSFX;
    [SerializeField] float diamondTimeScore = 0.2f;
    [SerializeField] float animationSpeed = 1f;
    Animator diamondAnimator;

    void Start()
    {
        diamondAnimator = GetComponent<Animator>();
    }
    void Update()
    {

        CorrectAnimSpeed();
    }

    private void CorrectAnimSpeed()
    {
            animationSpeed = 1f/ Time.timeScale;

        diamondAnimator.SetFloat("DiaSpeed", animationSpeed);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager manager = FindObjectOfType<AudioManager>();
        if(manager != null)
            manager.Play("Diamond");
        else
            AudioSource.PlayClipAtPoint(diamondPickupSFX, Camera.main.transform.position);

        FindObjectOfType<GameSession>().AddScore(diamondPoints); // increase the score whenever player collects a diamond

        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if(timeManager != null)
        {
            timeManager.slowDownFactor += diamondTimeScore;


        }
        EmitScript escript = FindObjectOfType<EmitScript>();
        if (escript != null)
        {

            escript.GainPowerup();
        }

        Destroy(gameObject); // Make the diamond disappear, when player touches them
    }
}
