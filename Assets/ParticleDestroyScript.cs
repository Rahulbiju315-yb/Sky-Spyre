using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D collider = collision.GetComponent<Collider2D>();
        if(collider.name == "LitParticles")
        {
            collider.gameObject.active = false;
        }
    }
}
