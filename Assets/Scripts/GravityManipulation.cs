using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManipulation : MonoBehaviour
{
    GameObject player ;
    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Rigidbody rb = player.GetComponent<Rigidbody>();
        // Vector3 direction = player.transform.position - transform.position;
        // rb.velocity  = new Vector2(direction.x, direction.y).normalized * force;

        // float rot = Mathf.Atan2(-direction.y, -direction.x)*Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0, 0, rot+90);
    }

    // Update is called once per frame
    void Update()
    {
        timer+= Time.deltaTime;
        if(timer > 5){
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.gravityScale=1;
            timer = 0;  
            return; 
        }
    }
    public void ButtonClicked(){
        
        timer=Time.deltaTime;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.gravityScale=-1;
    }
}
