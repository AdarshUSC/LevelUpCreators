using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boomerang : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 throwDirection;
    private bool isThrown = false;
    private float throwForce = 10.0f;
    private GameObject hitTree;

    private float blueTimer;
    private float greenTimer;
    private bool blueOn;
    private bool greenOn;
    private GameObject colorPanel;
    GameObject player;

    GameObject collidedEnemy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
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
        blueTimer = 0f;  
        greenTimer = 0f;
        colorPanel = GameObject.FindGameObjectWithTag("CommonCanvas").transform.Find("ColorPanel").gameObject;
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
        if(blueOn){
            blueTimer+= Time.deltaTime;
            if(blueTimer > 5){
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                GameObject icePrefab = collidedEnemy.transform.Find("iceCave").gameObject;
                icePrefab.GetComponent<SpriteRenderer>().enabled = false;
                foreach(Transform transform in colorPanel.transform) {
                    if(transform.CompareTag("ColorButton")) {
                        Button colorButton = transform.gameObject.GetComponent<Button>();
                        // colorButton.interactable=true based on colors count;
                    }
                }
                GameObject.FindGameObjectWithTag("mixArea").GetComponent<Button>().interactable=false;
                blueOn=false;
                blueTimer = 0f;
            } 
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        Color currentColor = GetCurrentColor();
        Debug.Log("Current color boomerang"+ currentColor);

        if (collision.gameObject.CompareTag("Tree"))
        {
            transform.position = transform.parent.position;
            gameObject.SetActive(false);

            Debug.Log("I am hit the tree");
            hitTree = collision.gameObject;

            hitTree.GetComponent<Tree>().DropFruits();

        } else if (collision.gameObject.CompareTag("Enemy") ){
            
            collidedEnemy = collision.gameObject;
            Debug.Log("enemy hit");
            Color currColor = colorPanel.transform.Find("mixArea").gameObject.GetComponent<Image>().color;
            if(currColor==Color.blue){
                blueOn=true;
                GameObject icePrefab = collidedEnemy.transform.Find("iceCave").gameObject;
                icePrefab.GetComponent<SpriteRenderer>().enabled = true;
                blueTimer=Time.deltaTime;
                foreach(Transform transform in colorPanel.transform) {
                    if(transform.CompareTag("ColorButton")) {
                        Button colorButton = transform.gameObject.GetComponent<Button>();
                        colorButton.interactable=false;
                    }
                }
            }
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Bush")){
            transform.position = transform.parent.position;
            hitTree = collision.gameObject;
            hitTree.GetComponent<Tree>().DropFruits();
            player.GetComponent<SpriteRenderer>().color = Color.green;
        } else if (collision.gameObject.layer == LayerMask.NameToLayer("Default")){
            transform.position = transform.parent.position;
            gameObject.SetActive(false);
        }
    }
    Color GetCurrentColor()
    {
        Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;
        Debug.Log("Current color:----- " + currentColor);
        return currentColor;
    }
}









