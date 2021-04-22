using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitEnding : MonoBehaviour
{
    GameObject canvas;
    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvas.SetActive(true);
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
         canvas.SetActive(false);
    }
}
