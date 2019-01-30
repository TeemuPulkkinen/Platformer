using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    // access to the text element that shows score value
    public Text scoreValue;

    // access to the high score value
    public Text highScoreValue;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set the text of our score value and converts int toString
        scoreValue.text = GameManager.instance.score.ToString();
        // set the text of our high score value
        highScoreValue.text = GameManager.instance.highScore.ToString();
    }

    // sends player back to level 1
    public void RestartGame()
    {
        GameManager.instance.ResetGame();
        SceneManager.LoadScene("Level1");
    }
}
