using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boomerang : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 throwDirection;
    private bool isThrown = false;
    private float throwForce = 20.0f;
    private GameObject hitTree;
    GameObject player;

    void Start()
    {
        // rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        // if (player != null)
        // {
        //     transform.parent = player.transform;
        // }
        // else
        // {
        //     Debug.LogError("Player GameObject not found. Make sure to set the correct player object name.");
        // }
        // rb.velocity = Vector2.zero;
        // rb.angularVelocity = 0f;
        // gameObject.SetActive(false);
    }

    public void Throw(Vector2 direction)
    {
        // gameObject.SetActive(true);
        // transform.position = transform.parent.position;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
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
    }

    void OnCollisionEnter2D(Collision2D collision) {

        Color currColor =  gameObject.GetComponent<SpriteRenderer>().color;
        if (collision.gameObject.CompareTag("Tree") && currColor == Color.green){
            Debug.Log("tree is hit w green bullet with "+ collision.gameObject+" and "+gameObject);
            player.GetComponent<SpriteRenderer>().color = Color.green;
            Player.current_mechs.Add("Camouflage");
            Tree.greenOn=true;
            Tree.greenTimer=Time.deltaTime;
            Player.playerMoveSpeed=3.6f;
        } else if (collision.gameObject.CompareTag("Tree") && currColor == Color.red){
            Player.current_mechs.Add("Tree Hit");
            Debug.Log("tree has been hit");
            hitTree = collision.gameObject;
            hitTree.GetComponent<Tree>().DropFruits();
        } else if (collision.gameObject.CompareTag("Enemy") && currColor == Color.blue){
            Debug.Log("enemy hit");
            EnemyPatrol.blueOn=true;
            EnemyPatrol.blueTimer=Time.deltaTime;
            Player.current_mechs.Add("Froze Enemy");
            GameObject icePrefab = collision.gameObject.transform.Find("iceCave").gameObject;
            icePrefab.GetComponent<SpriteRenderer>().enabled = true;
            // gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            // collision.gameObject.transform.position = collision.gameObject.transform.parent.position;
            // collision.gameObject.gameObject.SetActive(false);
        }
        Destroy(gameObject);
        // Color currentColor = GetCurrentColor();
        // Debug.Log("Current color boomerang"+ currentColor);
        // Color currColor = gameObject.GetComponent<SpriteRenderer>().color;

        // if (collision.gameObject.CompareTag("Tree") && currColor == Color.red)
        // {
        //     // transform.position = transform.parent.position;
        //     // gameObject.SetActive(false);
        //     Player.current_mechs.Add("Tree Hit");
        //     Debug.Log("I am hit the tree");
        //     hitTree = collision.gameObject;

        //     hitTree.GetComponent<Tree>().DropFruits();

        // } else if (collision.gameObject.layer == LayerMask.NameToLayer("Default") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("TreeLayer"))
        // {
        //     // transform.position = transform.parent.position;
        //     // gameObject.SetActive(false);
        // }
        // Destroy(gameObject);
    }
    Color GetCurrentColor()
    {
        Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;
        Debug.Log("Current color:----- " + currentColor);
        return currentColor;
    }
}









