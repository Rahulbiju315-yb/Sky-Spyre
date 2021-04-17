using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWizard : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public int hit;
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f); // Check the sign of the velocity
        hit=0;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hit>=14 && dead ==false){
            dead=true;
            Debug.Log("dying");
            StartCoroutine(OnDeath());
        }
    }

    IEnumerator OnDeath(){
        gameObject.GetComponent<ProjectileSource>().Stop();
        animator.speed *= 0.8f;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(4f);
        Destroy(GameObject.Find("Boss"));
    } 

    public void Hit(){
        if(dead==false){
            animator.SetTrigger("Hit");
            hit++;
        }
    }
}


