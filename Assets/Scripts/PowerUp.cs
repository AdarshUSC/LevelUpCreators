using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player ;
    float powerUpTimer;
    
    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        powerUpTimer = 0;  
    }

    // Update is called once per frame
    void Update()
    {   
        if(Player.isPowerUpOn){
            powerUpTimer+= Time.deltaTime;
            if(powerUpTimer > 5){
                Player.isPowerUpOn=false;
                Player.playerMoveSpeed-=4.0f;
                powerUpTimer = 0;  
                return; 
            }
        }
        
    }
    public void ButtonClicked(){
        
        powerUpTimer=Time.deltaTime;
        Player.isPowerUpOn=true;
        Player.playerMoveSpeed+=4.0f;
    }
}
