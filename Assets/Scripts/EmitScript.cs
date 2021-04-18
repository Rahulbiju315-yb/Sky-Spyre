using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitScript : MonoBehaviour
{
    private Animator playerAnimator;
    private GameObject player;

    private float lastTime;
    private bool isAlive;
    
    public GameObject litPSprefab;
    public float emissionVelocity;

    static private bool hasBeenAcquired;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerAnimator = player.GetComponent<Animator>();
        lastTime = 0;
    }

    public void GainPowerup()
    {
        hasBeenAcquired = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenAcquired && !isAlive && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            lastTime = Time.time;
            litPSprefab = (GameObject)Instantiate(litPSprefab, player.transform.position, transform.rotation);
            litPSprefab.gameObject.active = true;

            Vector2 to = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 vel = (to - new Vector2(player.transform.position.x, player.transform.position.y)).normalized * emissionVelocity;
            litPSprefab.GetComponent<Rigidbody2D>().velocity = vel;
            litPSprefab.name = "LitParticles";
            isAlive = true;

        }

        if (isAlive && Time.time - lastTime > 1)
        {
            isAlive = false;
        }
    }
}
