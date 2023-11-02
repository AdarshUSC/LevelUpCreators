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


    void Start () {
        this.originalX = this.transform.position.x;
        wallLeft = transform.position.x - 1f;
        wallRight = transform.position.x + 1f;
    }

    // Update is called once per frame
    void Update () {
        GameObject icePrefab = gameObject.transform.FindChild("iceCave").gameObject;
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
}
// 115, 72, 140 // 178, 238, 46 // 55, 199, 163 