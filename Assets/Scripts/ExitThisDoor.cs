using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitThisDoor : MonoBehaviour
{

    [SerializeField] float secondsToLoad = 0.1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().SetTrigger("Open"); // To open the door
    }

    public void StartLoadingNextLevel()
    {



        GetComponent<Animator>().SetTrigger("Close"); // To close the door

        StartCoroutine(LoadNextLevel()); // Wait 2s before loading the next level
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(secondsToLoad);

        Time.timeScale = 1f;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // To get the current Scene

        SceneManager.LoadScene(currentSceneIndex + 1); 




    }




}
