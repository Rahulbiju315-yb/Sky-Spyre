using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHeart : MonoBehaviour
{
    [SerializeField] AudioClip heartPickupSFX;

    [SerializeField] int heartLives = 1; // No. of lives a player gets when they collect a heart
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(heartPickupSFX, Camera.main.transform.position);

        FindObjectOfType<GameSession>().AddLife(heartLives);
        
        Destroy(gameObject); // Make the heart disappear, when player touches them
    }
}
