using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] int score = 0;
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

    private void Start()
    {
        livesText.text = playerLives.ToString(); // Assign the score and lives to the textboxes
        scoreText.text = score.ToString();
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
        livesText.text = playerLives.ToString(); // Update the lives whenever the player is hit
    }

    public void AddLife(int value) // Update the lives whever player collects a heart
    {
        playerLives += value;
        livesText.text = playerLives.ToString();
    }
    public void AddScore(int value) // So we can assign different values to different diamonds etc.
    {
        score += value;
        scoreText.text = score.ToString(); // Update the score whenever the player collects a diamond
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject); // Destroy the current Game session, to reset the lives and the score
    }
}
