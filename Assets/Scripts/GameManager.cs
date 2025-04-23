/*****************************************************************************
// File Name :         GameManager.cs
// Author :            Josh Bond
// Creation Date :     April 22, 2025
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

    // Start is called before the first frame update
    /// <summary>
    /// Starts the game at Scene 0 (Main Menu)
    /// </summary>
    void Start()
    {
        currentScene = 0;
    }

    /// <summary>
    /// Reduces the enemyCount by 1 each time an enemy is destroyed
    /// </summary>
    public void DestroyEnemy()
    {
        enemyCount -= 1;
    }

    // Update is called once per frame
    /// <summary>
    /// Updates the enemyCount, changes the scene when all enemies have been defeated, and allows the player to make
    /// progress or lose the game
    /// </summary>
    void Update()
    {
        enemiesText.text = "Enemies Remaining: " + enemyCount;
        
        if (enemyCount == 0)
        {

            if (currentScene == 0)
            {
                currentScene += 1;
                SceneManager.LoadScene(2);
            }
            else
            {
                wlText.text = "Win";
            }
        }

        if (player == null)
        {
            wlText.text = "Lose";
        }
    }
}