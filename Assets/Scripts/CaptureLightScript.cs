using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CaptureLightScript : MonoBehaviour
{
    public GameObject litPSprefab;
    public GameObject gateOfDarkness;
    private bool isLit;
    // Start is called before the first frame update

    public bool IsLit()
    {
        return isLit;
    }

    void Start()
    {
        isLit = false;
        gameObject.GetComponent<Light2D>().enabled = false;
        gateOfDarkness.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D collider = collision.GetComponent<Collider2D>();
        Debug.Log(collider.name);
        if (collider.name == "LitParticles" && !isLit)
        {
            litPSprefab = (GameObject)Instantiate(litPSprefab, gameObject.transform.position, gameObject.transform.rotation);
            litPSprefab.GetComponent<ParticleSystem>().loop = true;
            gameObject.GetComponent<Light2D>().enabled = true;
            if(gateOfDarkness != null)
            {
                gateOfDarkness.SetActive(false);
            }
            isLit = true;
        }
    }
}
