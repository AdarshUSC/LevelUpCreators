using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quicksand : MonoBehaviour
{
    private bool isSinking = false;
    private float sinkSpeed = 0.5f;
    //private float sinkDepth = 2.0f;
    private Transform playerTransform;
    private Transform scale1;
    private float startingYPosition;
    private Player p;
    private float groundYPosition;
    float sinkingProgress = 1f;
    private Vector3 originalPlayerScale;
    private Vector3 originalGroundPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("COLLISION");
            isSinking = true;
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                //Debug.Log("PLAYER1");
                //playerRigidbody.gravityScale = 0.001f; 
                //playerRigidbody.drag = 20f;
                sinkingProgress = 0f;
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
        GameObject player = GameObject.FindWithTag("Player");
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        if (player != null)
        {
            playerTransform = player.transform;
            startingYPosition = playerTransform.position.y;
            originalPlayerScale = playerTransform.localScale;
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            originalGroundPosition = hit.point;
        }
    }

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        startingYPosition = playerTransform.position.y;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            groundYPosition = hit.point.y;
        }
        /*if (isSinking && playerTransform != null)
        {
            Vector3 playerPosition = playerTransform.position;
            // playerPosition.y -= sinkSpeed * Time.deltaTime;
            //playerPosition.y = Mathf.Max(playerPosition.y, initialPosition.y - sinkDepth);
            playerTransform.position = playerPosition;
        }*/
        if (sinkingProgress < 1.0f)
        {
            sinkingProgress += Time.deltaTime * sinkSpeed; // Adjust sinkingSpeed as needed.
            Vector3 newPosition = playerTransform.position;
            newPosition.y = Mathf.Lerp(startingYPosition, groundYPosition, sinkingProgress);
            playerTransform.position = newPosition;
            float scale = Mathf.Lerp(1f, 0f, sinkingProgress); // Adjust the scale values as needed.
            playerTransform.localScale = new Vector3(1f, scale, 1f);

            Vector3 newGroundPosition = transform.position;
            newGroundPosition.y = Mathf.Lerp(originalGroundPosition.y, originalGroundPosition.y, sinkingProgress); // You want to keep the ground position the same
            transform.position = newGroundPosition;
        }
        if (sinkingProgress > 1.0f)
        {
            //Destroy(player);
            sinkingProgress = 1f;
            gameObject.transform.position = p.respawnPoint;
            playerTransform.localScale = originalPlayerScale;
            transform.position = originalGroundPosition;
            Debug.Log("Player SUNK");
        }
    }
}
