/*****************************************************************************
// File Name :         PlayerController.cs
// Author :            Josh Bond
// Creation Date :     April 22, 2025
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
    private InputAction move;
    public InputAction restart;
    public InputAction escape;
    public InputAction leftPunch;
    public InputAction rightPunch;

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

        leftPunch.started += Handle_LeftPunch;
        rightPunch.started += Handle_RightPunch;
    }

    private void Handle_EscapeStarted(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    private void Handle_RestartStarted(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Handle_LeftPunch(InputAction.CallbackContext context)
    {
        //Instantiate(LFist, LFist.position, transform.rotation);
    }

    private void Handle_RightPunch(InputAction.CallbackContext context)
    {
        //Instantiate(RFist, RFist.position, transform.rotation);
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
            health = 0;
        }
    }

    // Update is called once per frame
    /// <summary>
    /// Updates the health tracker and the player's position and checks if the player has enough health to continue
    /// </summary>
    void Update()
    {
        //move = playerMovement.ReadValue<Vector3>();

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
            Destroy(gameObject);
        }
    }
}