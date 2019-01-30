using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    // score text label
    public Text scoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        // Start with the correct score
        ResetHUD();
    }
    // Show up to date stats of the player
    public void ResetHUD()
    {
        scoreLabel.text = "Score: " + GameManager.instance.score;
    }
   
}
