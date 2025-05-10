/*****************************************************************************
// File Name :         PlayerController.cs
// Author :            Josh Bond
// Creation Date :     May 10, 2025
//
// Brief Description : Controls the character that is controlled by the player.
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float groundDistance;
    [SerializeField] private float health;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Rigidbody LFist;
    [SerializeField] private Rigidbody RFist;
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject quitButton;
    private InputAction move;
    public InputAction restart;
    public InputAction escape;
    public InputAction pause;
    public InputAction leftPunch;
    public InputAction rightPunch;
    public GameManager gameManager;
    private Rigidbody rb;
    private Vector3 playerMovement;

    // Start is called before the first frame update
    /// <summary>
    /// Sets the player's Rigidbody and input action map and disables the cursor during gameplay
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput.currentActionMap.Enable();

        restart = playerInput.currentActionMap.FindAction("Restart");
        escape = playerInput.currentActionMap.FindAction("Escape");
        pause = playerInput.currentActionMap.FindAction("Pause");

        restart.started += Handle_RestartStarted;
        escape.started += Handle_EscapeStarted;
        pause.started += Handle_PauseStarted;

        leftPunch.started += Handle_LeftPunch;
        rightPunch.started += Handle_RightPunch;

        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            Cursor.visible = false;
        }
    }

    /// <summary>
    /// Allows the player to exit out of the game
    /// </summary>
    /// <param name="context"></param>
    private void Handle_EscapeStarted(InputAction.CallbackContext context)
    {
        gameManager.Quit();
    }

    /// <summary>
    /// Allows the player to restart the current level
    /// </summary>
    /// <param name="context"></param>
    private void Handle_RestartStarted(InputAction.CallbackContext context)
    {
       gameManager.Restart();
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    /// <param name="context"></param>
    private void Handle_PauseStarted(InputAction.CallbackContext context)
    {
        resumeButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    /// <summary>
    /// Starts the game again after a pause
    /// </summary>
    public void Unpause()
    {
        resumeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    /// <summary>
    /// Starts the Left Punch action
    /// </summary>
    /// <param name="context"></param>
    private void Handle_LeftPunch(InputAction.CallbackContext context){}

    /// <summary>
    /// Starts the Right Punch action
    /// </summary>
    /// <param name="context"></param>
    private void Handle_RightPunch(InputAction.CallbackContext context){}

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
            health = 0;
        }
    }

    // Update is called once per frame
    /// <summary>
    /// Updates the health tracker and the player's position and checks if the player has enough health to continue
    /// </summary>
    void Update()
    {
        Quaternion playerRotation = Quaternion.LookRotation (new Vector3 (playerMovement.x, playerMovement.y,
        playerMovement.z));

        if (playerMovement.x < 0 || playerMovement.z < 0)
        {
        rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, playerRotation, rotateSpeed *
        Time.deltaTime);
        }

        healthText.text = "Health: " + health;

        rb.velocity = new Vector3(playerMovement.x, rb.velocity.y, playerMovement.z);
        LFist.position = new Vector3(rb.position.x + 0.7f, rb.position.y, rb.position.z);
        RFist.position = new Vector3(rb.position.x - 0.7f, rb.position.y, rb.position.z);

        if (health <= 0)
        {
            restartButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            Cursor.visible = true;
            Destroy(gameObject);
        }
    }
}