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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerAnimator = player.GetComponent<Animator>();
        lastTime = 0;
        isAlive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            Debug.Log("is alive" + isAlive);
            lastTime = Time.time;
            litPSprefab = (GameObject)Instantiate(litPSprefab, player.transform.position, transform.rotation);

            Vector2 to = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 vel = (to - new Vector2(player.transform.position.x, player.transform.position.y)).normalized * emissionVelocity;
            litPSprefab.GetComponent<Rigidbody2D>().velocity = new Vector2(emissionVelocity, 0);
            isAlive = true;
        }

        if(isAlive && Time.time - lastTime > 1)
        {
            isAlive = false;
        }
    }
}
