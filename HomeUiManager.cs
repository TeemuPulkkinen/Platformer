using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUiManager : MonoBehaviour
{
    // starts our level 1
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
