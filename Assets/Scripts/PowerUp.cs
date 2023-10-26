using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
        GameObject player ;
    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer+= Time.deltaTime;
        if(timer > 5){
            Player.isPowerUpOn=false;
            Player.moveSpeed-=3.0f;
            timer = 0;  
            return; 
        }
    }
    public void ButtonClicked(){
        
        timer=Time.deltaTime;
        Player.isPowerUpOn=true;
        Player.moveSpeed+=3.0f;
    }
}
