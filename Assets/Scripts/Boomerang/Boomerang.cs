using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 throwDirection;
    private bool isThrown = false;
    private float throwForce = 10.0f; 

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

        gameObject.SetActive(false);
    }

    public void Throw(Vector2 direction)
    {
        Debug.Log("I am in throw");
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

            float maxTravelDistance = 10.0f;

            if (distanceTravelled >= maxTravelDistance)
            {
                rb.velocity = -throwDirection * throwForce * 2.0f; 
                isThrown = false;
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
        if (transform.parent != null)
        {
            transform.position = transform.parent.position;
        }
    }
}
