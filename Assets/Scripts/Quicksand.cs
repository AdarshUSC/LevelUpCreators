using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quicksand : MonoBehaviour
{
    private bool isSinking = false;
    private float sinkSpeed = 0.05f;
    private float sinkDepth = 2.0f;
    private Transform playerTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            Debug.Log("COLLISION");
            isSinking = true;
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Debug.Log("PLAYER1");
                playerRigidbody.gravityScale = 0.001f; 
                playerRigidbody.drag = 10f;
            }
        }
    }
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("EXIT");
        if (collision.CompareTag("Player1"))
        {
            isSinking = false;
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.gravityScale = 1f;
                playerRigidbody.drag = 0f;
            }
        }
    }*/
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player1");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player1");
        playerTransform = player.transform;
        if (isSinking && playerTransform != null)
        {
            Vector3 playerPosition = playerTransform.position;
            // playerPosition.y -= sinkSpeed * Time.deltaTime;
            //playerPosition.y = Mathf.Max(playerPosition.y, initialPosition.y - sinkDepth);
            playerTransform.position = playerPosition;
        }
    }
}
