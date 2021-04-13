using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject globalLight;

    void Start()
    {
        globalLight = GameObject.Find("GlobalLight");
    }

    // Update is called once per frame
    void Update()
    {
        float sinx = Mathf.Sin(Time.time);
        globalLight.GetComponent<Light2D>().intensity = 2 * sinx * sinx + 0.5f;
    }
}
