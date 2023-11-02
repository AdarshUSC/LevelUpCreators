using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boomerang : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 throwDirection;
    private bool isThrown = false;
    private float throwForce = 10.0f;
    private GameObject hitTree;

    private int blueTimer;

    private int greenTimer;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            transform.parent = player.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found. Make sure to set the correct player object name.");
        }
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        gameObject.SetActive(false);
        blueTimer = 0;  
        greenTimer = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Throw(Vector2 direction)
    {
        gameObject.SetActive(true);
        transform.position = transform.parent.position;
        throwDirection = direction.normalized;
        rb.velocity = throwDirection * throwForce;
        isThrown = true;
    }

    void Update()
    {
        if (isThrown)
        {
            //float distanceTravelled = Vector2.Distance(transform.position, transform.parent.position);
            //float maxTravelDistance = 6.0f;

            //if (distanceTravelled >= maxTravelDistance)
            //{
            //    Renderer renderer = GetComponent<Renderer>();
            //    renderer.enabled = false;
            //}
            //if (distanceTravelled < 0.6f)
            //{
            //    Renderer renderer = GetComponent<Renderer>();
            //    renderer.enabled = false;
            //}
            //else
            //{
                Renderer renderer = GetComponent<Renderer>();
                renderer.enabled = true;
           // }
        } 
        // if(iceOn){
            // blueTimer+= Time.deltaTime;
            // if(blueTimer > 5){
            //     Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            //     rb.gravityScale=1;
            //     gravityTimer = 0;  
            //     antiGravityButton.interactable=true;
            //     return; 
            // }
        // }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Color currentColor = GetCurrentColor();
        Debug.Log("Current color boomerang"+ currentColor);

        if (collision.gameObject.CompareTag("Tree"))
        {
            transform.position = transform.parent.position;
            gameObject.SetActive(false);

            Debug.Log("I am hit the tree");
            hitTree = collision.gameObject;

            hitTree.GetComponent<Tree>().DropFruits();

        } else if (collision.gameObject.CompareTag("Enemy")){
            
            Debug.Log("enemy hit");
            GameObject icePrefab = collision.transform.Find("iceCave").gameObject;
            icePrefab.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else if (collision.gameObject.layer == LayerMask.NameToLayer("Default")){
            transform.position = transform.parent.position;
            gameObject.SetActive(false);
        }
    }

    Color GetCurrentColor()
    {
        Color currentColor = spriteRenderer.color;
        Debug.Log("Current color:----- " + currentColor);
        return currentColor;
    }
}









