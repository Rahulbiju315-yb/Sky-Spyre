using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] float pauseDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetTrigger("Open");
        StartCoroutine(pauseBeforeCLose());
        GetComponent<Animator>().SetTrigger("Close");
    }

    IEnumerator pauseBeforeCLose()
    {
        yield  return new WaitForSeconds(pauseDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
