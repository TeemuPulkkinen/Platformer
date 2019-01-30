using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Score of the player
    public int score = 0;

    // High score variable
    public int highScore = 0;

    // Current level
    public int currentLevel = 1;

    // How many levels there are?
    public int highestLevel = 4;

    // HUD manager
    private HUDManager hudManager;

    // Static instance of the Game Manager can be accessed from anywhere
    public static GameManager instance;

    private void Awake()
    {
        // check that instance exists
        if (instance == null)
        {
            // assign instance to the current object
            // this-keyword refers to the current object
            instance = this;
        }

        // make sure that instance is equal to the current object
        else if (instance != this)
        {
            // find an object of type HudManager
            hudManager = FindObjectOfType<HUDManager>();

            // destroy the current game object - we only need 1 and we already have it
            Destroy(gameObject);
        }

        // don't destroy this object when changing scenes
        DontDestroyOnLoad(gameObject);

        // find object of type HudManager
        hudManager = FindObjectOfType<HUDManager>();
    }

    // increase the player score
    public void increaseScore(int amount)
    {
        
        // increase score by the amount
        score += amount;

        // update the HUD
        if (hudManager != null)
        {
            hudManager.ResetHUD();
        }
        
        // show the new score
        print("new score: " + score);

        // have we got new high score?
        if (score > highScore)
        {
            // if yes
            highScore = score;

            print("new high score: " + highScore);
        }
    }

    public void ResetGame()
    {
        // Resets our score
        score = 0;

        // update the HUD
        if (hudManager != null)
        {
            hudManager.ResetHUD();
        }

        // set the current level back to 1
        currentLevel = 1;

        // load the level 1 scene
        SceneManager.LoadScene("Level1");
    }

    // send the player to next level
    public void increaseLevel()
    {
        // check if there are more levels
        if (currentLevel < highestLevel)
        {
            // increase current level by 1
            currentLevel++;
        }
        else
        {
            // go back to level 1
            currentLevel = 1;
        }

        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}
