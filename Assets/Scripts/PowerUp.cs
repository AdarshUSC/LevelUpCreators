using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player ;
    float powerUpTimer;
    public Button powerUpButton;
    
    // Start is called before the first frame update
    void Start(){
        if(powerUpButton!=null){
            player = GameObject.FindGameObjectWithTag("Player");
            powerUpTimer = 0;  
            powerUpButton.interactable=false;
        }
        
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
                powerUpButton.interactable=true;
                return; 
            }
        }
        
    }
    public void ButtonClicked(){

        if(powerUpButton!=null){
            powerUpTimer=Time.deltaTime;
            Player.isPowerUpOn=true;
            Player.playerMoveSpeed+=4.0f;
            powerUpButton.interactable=false;
            Player.powerup++;
            Player.current_mechs.Add("Powerup");
        }
        
        

    }
}
