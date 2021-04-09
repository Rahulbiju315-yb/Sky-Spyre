using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitScript : MonoBehaviour
{
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem particles = gameObject.GetComponent<ParticleSystem>();
        Debug.Log("sdfds" + playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"));
        if (!particles.isEmitting && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            particles.Play();
        }
    }
}
