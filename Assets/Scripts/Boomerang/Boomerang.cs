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
            float distanceTravelled = Vector2.Distance(transform.position, transform.parent.position);
            float maxTravelDistance = 6.0f;

            if (distanceTravelled >= maxTravelDistance)
            {
                rb.velocity = -throwDirection * throwForce;
            }
            if (distanceTravelled < 0.6f)
            {
                Renderer renderer = GetComponent<Renderer>();
                renderer.enabled = false;
            }
            else
            {
                Renderer renderer = GetComponent<Renderer>();
                renderer.enabled = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            Debug.Log("I am hit the tree");
            // Detect collision with a tree
            hitTree = collision.gameObject;

            // Call the DropFruits method on the hit tree
            hitTree.GetComponent<Tree>().DropFruits();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            transform.position = transform.parent.position;
            gameObject.SetActive(false);
        }
    }
}









