using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEnter : MonoBehaviour
{
    public AudioClip bossBGM;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Collider2D>().name == "Player")
        {
            AudioSource asource = GameObject.Find("BGM1").GetComponent<AudioSource>();
            if (asource.clip != bossBGM)
            {
                asource.clip = bossBGM;
                asource.Play();
            }
        }
        GameObject.Find("Boss").GetComponent<ProjectileSource>().hasActivated = true;
    }
}
