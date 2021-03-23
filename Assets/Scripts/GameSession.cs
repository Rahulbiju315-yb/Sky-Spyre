using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;

    //Singleton Pattern

    private void Awake() // This method executes even before Start()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length; // Length of array of all Game Sessions

        if(numberOfGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    public void processPlayerDeath()
    {
        if (playerLives > 1) // If Player has 2 or more lives before, they're hit, subtract a life
        {
            SubtractLife();
        }
        else                 // Otherwise, restart the game
        {
            ResetGame();
        }
            

    }

    private void SubtractLife()
    {
        playerLives--;
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject); // Destroy the current Game session, to reset the lives and the score
    }
}
