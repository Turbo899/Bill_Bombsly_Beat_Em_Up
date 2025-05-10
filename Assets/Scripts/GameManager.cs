/*****************************************************************************
// File Name :         GameManager.cs
// Author :            Josh Bond
// Creation Date :     May 10, 2025
//
// Brief Description : Allows the player to progress through or lose the game
*****************************************************************************/
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text enemiesText;
    [SerializeField] private TMP_Text wlText;
    [SerializeField] private int enemyCount;
    [SerializeField] private int currentScene;
    [SerializeField] private GameObject player;
    public GameObject RestartButton;
    public GameObject QuitButton;

    /// <summary>
    /// Reduces the enemyCount by one each time an enemy is destroyed
    /// </summary>
    public void DestroyEnemy()
    {
        enemyCount -= 1;
    }

    /// <summary>
    /// Moves the game to the next level when one is completed
    /// </summary>
    public void NewScene()
    {
        if (enemyCount == 0)
        {
            if (currentScene > 0 && currentScene < 4)
            {
                currentScene += 1;
                SceneManager.LoadScene(currentScene);
            }
            else
            {
                RestartButton.gameObject.SetActive(true);
                QuitButton.gameObject.SetActive(true);
                Cursor.visible = true;
                wlText.text = "Win";
            }
        }
    }

    /// <summary>
    /// Restarts the level
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    /// <summary>
    /// Updates the enemyCount, changes the scene when all enemies have been defeated, and allows the player to make
    /// progress or lose the game
    /// </summary>
    void Update()
    {
        NewScene();

        enemiesText.text = "Enemies Remaining: " + enemyCount;

        if (player == null)
        {
            wlText.text = "Lose";
        }
    }
}