using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResize : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 originalScale;
    private bool ogscale;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mushroom")
        {
            Destroy(collision.gameObject);
            Vector3 newScale = new Vector3(3.0f, 3.0f, 3.0f);
            playerTransform.localScale = newScale;
            ogscale = false;
        }
    }
        // Start is called before the first frame update
        void Start()
    {
        playerTransform = GetComponent<Transform>();
        originalScale = playerTransform.localScale;
        ogscale = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
