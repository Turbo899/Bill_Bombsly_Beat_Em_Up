/*****************************************************************************
// File Name :         FistController.cs
// Author :            Josh Bond
// Creation Date :     March 31, 2025
//
// Brief Description : Controls the players fists that are used to attack enemies.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FistController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float fistSpeed;
    [SerializeField] private Rigidbody LFist;
    [SerializeField] private Rigidbody RFist;
    [SerializeField] private Rigidbody player;
    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    /// <summary>
    /// Sets the Rigidbodies for both fists and sets playerInput as the action map that controls both of the fists
    /// </summary>
    void Start()
    {
        LFist = GetComponent<Rigidbody>();
        RFist = GetComponent<Rigidbody>();
        playerInput.currentActionMap.Enable();
    }

    /// <summary>
    /// Moves the left fist forward and back
    /// </summary>
    /// <returns></returns>
    public IEnumerator LeftPunch()
    {
        LFist.AddForce(transform.forward * -fistSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1/60f);
        LFist.AddForce(transform.forward * fistSpeed * Time.deltaTime);

    }

    /// <summary>
    /// Moves the right fist forward and back
    /// </summary>
    /// <returns></returns>
    public IEnumerator RightPunch()
    {
        RFist.AddForce(transform.forward * -fistSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1/60f);
        RFist.AddForce(transform.forward * fistSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Activates the left fist punch
    /// </summary>
    private void OnLeftPunch()
    {
        StartCoroutine(LeftPunch());
    }

    /// <summary>
    /// Activates the right fist punch
    /// </summary>
    private void OnRightPunch()
    {
        StartCoroutine(RightPunch());
    }

    /// <summary>
    /// Destroys an enemy when it collides with a fist
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            gameManager.DestroyEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float Distance = Vector3.Distance(transform.position, player.transform.position);
        }
    }
}