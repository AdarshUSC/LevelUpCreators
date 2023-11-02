using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedPath : MonoBehaviour
{
    private int jumpsRequired = 3;  // Number of jumps required to destroy the ground
    private int jumps = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player jumped");
            jumps++;
            if (jumps >= jumpsRequired)
            {
                Destroy(gameObject);
                jumps = 0;// Destroy the ground object
            }
        }
    }
            // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
