using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // Start is called before the first frame update
    public float walkSpeed = 2.0f;      // Walkspeed
    public float wallLeft;       // Define wallLeft
    public float wallRight ;      // Define wallRight
    float walkingDirection = 1.0f;
    Vector2 walkAmount;
    float originalX; // Original float value

    float blueTimer;
    bool blueOn;


    void Start () {
        this.originalX = this.transform.position.x;
        wallLeft = transform.position.x - 1f;
        wallRight = transform.position.x + 1f;
        blueTimer = 0f;
        blueOn = false;
    }

    // Update is called once per frame
    void Update () {
        GameObject icePrefab = gameObject.transform.FindChild("iceCave").gameObject;
        if(blueOn){
            blueTimer+= Time.deltaTime;
            Debug.Log("blue timer is "+ blueTimer);
            if(blueTimer > 5){
                blueOn=false;
                blueTimer = 0.0f;
                icePrefab.GetComponent<SpriteRenderer>().enabled = false;
                // foreach(Transform transform in colorPanel.transform) {
                //     if(transform.CompareTag("ColorButton")) {
                //         Button colorButton = transform.gameObject.GetComponent<Button>();
                //         colorButton.interactable=true; // switching off the colors when power up is being used
                //     }
                // }
                // GameObject.FindGameObjectWithTag("mixArea").GetComponent<Button>().interactable=false;
                // gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                return;
            } 
        }
        SpriteRenderer sr = icePrefab.GetComponent<SpriteRenderer>();
        if(!sr.enabled){
            walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
            if (walkingDirection > 0.0f && transform.position.x >= wallRight) {
                walkingDirection = -1.0f;
            } else if (walkingDirection < 0.0f && transform.position.x <= wallLeft) {
                walkingDirection = 1.0f;
            }
            transform.Translate(walkAmount);
        } 
    }
    void OnCollisionEnter2D(Collision2D collision) {
        
        Color currColor =  collision.gameObject.GetComponent<SpriteRenderer>()!=null?collision.gameObject.GetComponent<SpriteRenderer>().color:Color.white;
        if (collision.gameObject.CompareTag("Boomerang") && currColor == Color.blue){
            Debug.Log("enemy hit");
            blueOn=true;
            blueTimer=Time.deltaTime;
            GameObject icePrefab = gameObject.transform.FindChild("iceCave").gameObject;
            icePrefab.GetComponent<SpriteRenderer>().enabled = true;
            // gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            collision.gameObject.transform.position = collision.gameObject.transform.parent.position;
            collision.gameObject.gameObject.SetActive(false);
            // Destroy(gameObject);
        }
    }
}
// 115, 72, 140 // 178, 238, 46 // 55, 199, 163 