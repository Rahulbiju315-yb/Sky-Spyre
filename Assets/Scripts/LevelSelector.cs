using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void LoadTimeLevel()
    {
        SceneManager.LoadScene(2); // Make sure Time level is second
    }

    public void LoadDarknessLevel()
    {
        SceneManager.LoadScene(3); // Make sure Darkness level is second
    }

    public void LoadGravityLevel()
    {
        SceneManager.LoadScene(4); // Make sure Gravity level is second
    }



}
