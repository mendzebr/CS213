using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    public void Update()
    {
     
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("SpaceGhostSheepGame");
        
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    public void BackButton()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
