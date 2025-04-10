/*****************************************************************************
// File Name :         BasicEnemyController.cs
// Author :            Josh Bond
// Creation Date :     March 31, 2025
//
// Brief Description : Controls the game's basic enemy that moves towards the player.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float detectionRange;
    private Rigidbody rb;

    // Start is called before the first frame update
    /// <summary>
    /// Sets the enemy and its Ridigbody
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    /// <summary>
    /// Moves the enemy towards the player when the player is close
    /// </summary>
    private void FixedUpdate()
    {
        if (player != null && rb != null)
        {
            float Distance = Vector3.Distance(transform.position, player.transform.position);

            if (Distance <= detectionRange)
            {
                Vector3 Direction = (player.transform.position - transform.position).normalized;
                rb.MovePosition(rb.position + Direction * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
}