using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);// make sure level selector is level 1 in build settings
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
