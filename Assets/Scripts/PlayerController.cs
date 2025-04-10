/*****************************************************************************
// File Name :         PlayerController.cs
// Author :            Josh Bond
// Creation Date :     March 31, 2025
//
// Brief Description : Controls the character that is controlled by the player.
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float groundDistance;
    [SerializeField] private float health;
    [SerializeField] private TMP_Text healthText;
    public InputAction restart;
    public InputAction escape;

    private Rigidbody rb;
    private Vector3 playerMovement;

    // Start is called before the first frame update
    /// <summary>
    /// Sets the player's Rigidbody and input action map
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput.currentActionMap.Enable();

        restart = playerInput.currentActionMap.FindAction("Restart");
        escape = playerInput.currentActionMap.FindAction("Escape");

        restart.started += Handle_RestartStarted;
        escape.started += Handle_EscapeStarted;
    }

    private void Handle_EscapeStarted(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    private void Handle_RestartStarted(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Moves the player up when the jump key is pressed and the player is on the ground
    /// </summary>
    void OnJump()
    {
        if (onGround() == true)
        {
            rb.velocity = new Vector3(0, jumpSpeed, 0);
        }
    }

    /// <summary>
    /// Checks if the player is on the ground
    /// </summary>
    /// <returns></returns>
    bool onGround()
    {
        if(Physics.Raycast(rb.position, Vector3.down, groundDistance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Moves the player
    /// </summary>
    /// <param name="moveValue"></param>
    private void OnMove(InputValue moveValue)
    {
        Vector2 playerMovementValue = moveValue.Get<Vector2>();
        playerMovement.x = playerMovementValue.x * moveSpeed;
        playerMovement.z = playerMovementValue.y * moveSpeed;
    }

    /// <summary>
    /// Causes the player to gain or lose health based on contact with enemies or milkshakes
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && gameObject.name == ("Player"))
        {
            health -= 25;
        }

        if (collision.gameObject.tag == "Health")
        {
            Destroy(collision.gameObject);
            health += 25;
        }

        if (collision.gameObject.tag == "Killbox")
        {
            Destroy(gameObject);
        }
    }
    
    // Update is called once per frame
    /// <summary>
    /// Updates the health tracker and the player's position and checks if the player has enough health to continue
    /// </summary>
    void Update()
    {
        healthText.text = "Health: " + health;

        rb.velocity = new Vector3(playerMovement.x, rb.velocity.y, playerMovement.z);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}