/*****************************************************************************
// File Name :         GameManager.cs
// Author :            Josh Bond
// Creation Date :     March 31, 2025
//
// Brief Description : Should be used in the future.
*****************************************************************************/
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text enemiesText;
    [SerializeField] private int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DestroyEnemy()
    {
        enemyCount -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesText.text = "Enemies Remaining: " + enemyCount;
    }
}