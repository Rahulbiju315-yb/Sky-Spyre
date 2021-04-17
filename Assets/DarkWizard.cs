using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWizard : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    int hit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f); // Check the sign of the velocity
        hit=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(hit==5){
            StartCoroutine(OnDeath());
        }
    }

    IEnumerator OnDeath(){
        gameObject.GetComponent<ProjectileSource>().Stop();
        animator.speed *= 0.25f;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(4f);
        Destroy(GameObject.Find("Boss"));
    }
}


