using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class lightning : MonoBehaviour
{
    private float lastTime = 0;
    public float deltaOn = 2;
    public float deltaOff = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastTime > deltaOn && !gameObject.GetComponent<Light2D>().enabled)
        {
            gameObject.GetComponent<Light2D>().enabled = true;
            lastTime = Time.time;
        }
        else if (Time.time - lastTime > deltaOff && gameObject.GetComponent<Light2D>().enabled)
        {
            gameObject.GetComponent<Light2D>().enabled = false;
            lastTime = Time.time;
        }
        else if(gameObject.GetComponent<Light2D>().enabled)
        {
            float delta = Time.time - lastTime;
            gameObject.GetComponent<Light2D>().intensity = 5 * Mathf.Exp(- (5 * delta - 0.01f) * (5 * delta - 0.01f));
        }
    }
}
