using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondGravity : MonoBehaviour
{
    [SerializeField] AudioClip diamondPickupSFX;
    public Rigidbody2D rb;    
    public Transform tr;    
    public Vector3 set;
    public Gravity script;

    [SerializeField] int diamondPoints = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(diamondPickupSFX, Camera.main.transform.position); // The camera is following the player, so this works

        FindObjectOfType<GameSession>().AddScore(diamondPoints); // increase the score whenever player collects a diamond
        script.g = !script.g;
        Destroy(gameObject); // Make the heart disappear, when player touches them
        
    }
}
