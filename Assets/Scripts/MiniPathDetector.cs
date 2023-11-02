using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPathDetector : MonoBehaviour
{
    public bool playerInside = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Player entered the mini path.");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Player exited the mini path.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerInside = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
