using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityManipulation : MonoBehaviour
{
    GameObject player ;
    float gravityTimer;
    public Button antiGravityButton;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        if(antiGravityButton!=null){
            antiGravityButton.interactable=true;
            gravityTimer= Time.deltaTime;
        }
        
        // Rigidbody rb = player.GetComponent<Rigidbody>();
        // Vector3 direction = player.transform.position - transform.position;
        // rb.velocity  = new Vector2(direction.x, direction.y).normalized * force;

        // float rot = Mathf.Atan2(-direction.y, -direction.x)*Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0, 0, rot+90);
    }

    // Update is called once per frame
    void Update()
    {   
        if(antiGravityButton!=null){
            gravityTimer+= Time.deltaTime;
            if(gravityTimer > 5){
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                rb.gravityScale=1;
                gravityTimer = 0;  
                antiGravityButton.interactable=true;
                return; 
        }
        }
        
    }
    public void ButtonClicked(){

        if(antiGravityButton!=null){
            gravityTimer=Time.deltaTime;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.gravityScale=-1;
            Player.antigravity++;
            Player.current_mechs.Add("Antigravity");
            antiGravityButton.interactable=false;
        }
        
    }
}
