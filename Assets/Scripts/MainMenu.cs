/*****************************************************************************
// File Name :         GameManager.cs
// Author :            Josh Bond
// Creation Date :     May 10, 2025
//
// Brief Description : Controls the game's start/main menu.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameManager gameManager;

    /// <summary>
    /// Starts the game
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}