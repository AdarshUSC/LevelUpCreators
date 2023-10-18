using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boomerang : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 throwDirection;
    private bool isThrown = false;
    private float throwForce = 10.0f;
    public Vector2 windDirection = Vector2.zero;
    public float spinSpeed = 10f;
    public float liftCoefficient = 0.2f;
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (transform.parent != null)
            { 
                transform.position = transform.parent.position;
            }

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Debug.Log("Mans000 Default");
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
}









