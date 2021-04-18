using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void LoadTimeLevel()
    {
        SceneManager.LoadScene("TIme Horizontal"); // Make sure Time level is second
    }

    public void LoadDarknessLevel()
    {
        SceneManager.LoadScene("Dark"); // Make sure Darkness level is second
    }

    public void LoadGravityLevel()
    {
        SceneManager.LoadScene("Gravity"); // Make sure Gravity level is second
    }



}
