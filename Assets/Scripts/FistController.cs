/*****************************************************************************
// File Name :         FistController.cs
// Author :            Josh Bond
// Creation Date :     May 10, 2025
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
    private float fistTime;
    [SerializeField] private Rigidbody LFist;
    [SerializeField] private Rigidbody RFist;
    [SerializeField] private Rigidbody player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerController playerController;
    private bool isPunching;

    // Start is called before the first frame update
    /// <summary>
    /// Sets isPunching to false
    /// </summary>
    void Start()
    {
        isPunching = false;
    }

    /// <summary>
    /// Moves the fists forward (left) and back
    /// </summary>
    /// <returns></returns>
    public IEnumerator LeftPunch()
    {
        isPunching = true;

        while (fistTime > 0)
        {
            Debug.Log("Left");
            transform.Translate(0, (-Mathf.Sin(transform.position.y)), 0);
            fistTime -= 1;
            yield return new WaitForSeconds(0.01f);
        }
        isPunching = false;
    }

    /// <summary>
    /// Moves the fists forward (right) and back
    /// </summary>
    /// <returns></returns>
    public IEnumerator RightPunch()
   {
        isPunching = true;

        while (fistTime > 0)
        {
            Debug.Log("Right");
            transform.Translate(0, (Mathf.Sin(transform.position.y)), 0);
            fistTime -= 1;
            yield return new WaitForSeconds(0.01f);
        }
        isPunching = false;
    }

    /// <summary>
    /// Activates the left fist punch
    /// </summary>
    public void OnLeftPunch()
    {
        fistTime = 50;
        StartCoroutine(LeftPunch());
    }

    /// <summary>
    /// Activates the right fist punch
    /// </summary>
    public void OnRightPunch()
    {
        fistTime = 50;
        StartCoroutine(RightPunch());
    }

    /// <summary>
    /// Destroys an enemy when it collides with a fist
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (isPunching == true)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject);
                gameManager.DestroyEnemy();
            }

            if (collision.gameObject.tag == "Obstacle")
            {
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "Health")
            {
                //do nothing
            }
        }
        else
        {
            //do nothing
        }
    }

    // Update is called once per frame
    /// <summary>
    /// Keeps the fists on the player
    /// </summary>
    void Update()
    {
        if (player != null)
        {
            float Distance = Vector3.Distance(transform.position, player.transform.position);
        }
    }
}