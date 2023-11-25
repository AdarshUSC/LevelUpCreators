using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FruitCollector : MonoBehaviour
{

    public Image powerUpTimerImage; // Radial image for the power-up timer
    private float powerUpDuration = 10.0f; // Duration of the power-up
    GameObject player;
    float powerUpTimer;


    private  int collectibles = 0;
    [SerializeField] private TMP_Text CollectiblesText;

    [SerializeField] private int fruitsRequiredForPowerUp = 20; // 

    private KeyCode powerUpKey = KeyCode.C;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        powerUpTimer = 0;

        if (powerUpTimerImage != null)
        {
            powerUpTimerImage.fillAmount = 0;
        }
    }

    private void Update()
    {
        if (collectibles >= fruitsRequiredForPowerUp && Input.GetKeyDown(powerUpKey))
        {
            ActivatePowerUp();
        }

        if (Player.isPowerUpOn)
        {
            powerUpTimer += Time.deltaTime;
            if (powerUpTimerImage != null)
            {
                powerUpTimerImage.fillAmount = (powerUpDuration - powerUpTimer) / powerUpDuration;
            }
            if (powerUpTimer > powerUpDuration)
            {
                DeactivatePowerUp();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Debug.Log("I am on trigger enter 2D" + collision.gameObject.transform.position.y);
            Player.CollectablePoints.Add(collision.gameObject.transform.position);
            //Player.current_mechs.Add("Fruit Collected");
            Destroy(collision.gameObject);
            collectibles++;
            CollectiblesText.text = collectibles.ToString();
            //
        } 
        // else if (collision.gameObject.CompareTag("ColorCollectible")){
            
        //     // Debug.Log("I am on color enter 2D" + collision.gameObject.transform.position.y);
        //     SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
        //     Destroy(collision.gameObject);
        //     if(sr.color==Color.red){
        //         Player.redCollected++;
        //     } else if(sr.color==Color.green){
        //         Player.greenCollected++;
        //     } else if(sr.color==Color.blue){
        //         Player.blueCollected++;
        //     }
        //     // Debug.Log("Colors Collected :"+ Player.redCollected+","+Player.greenCollected+","+Player.blueCollected);
        // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Debug.Log("I am on trigger enter 2D"+ collision.gameObject.transform.position.y);
            Player.CollectablePoints.Add(collision.gameObject.transform.position);
            //Player.current_mechs.Add("Fruit Collected");
            Destroy(collision.gameObject);
            collectibles++;
            CollectiblesText.text = collectibles.ToString();
            //

        } else if (collision.gameObject.CompareTag("ColorCollectible")){
            
            // Debug.Log("I am on color enter 2D" + collision.gameObject.transform.position.y);
            SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
            //Player.current_mechs.Add("Color Collected");
            Destroy(collision.gameObject);
            if(sr.color==Color.red){
                Player.redCollected++;
            } else if(sr.color==Color.green){
                Player.greenCollected++;
            } else if(sr.color==Color.blue){
                Player.blueCollected++;
            }
            // Debug.Log("Colors Collected :"+ Player.redCollected+","+Player.greenCollected+","+Player.blueCollected);
        }
    }

    private void ActivatePowerUp()
    {
        if (!Player.isPowerUpOn)
        {
            Player.isPowerUpOn = true;
            Player.playerMoveSpeed += 4.0f;
            powerUpTimer = 0;
            Player.powerup++;
            Player.current_mechs.Add("Powerup");

            if (powerUpTimerImage != null)
            {
                powerUpTimerImage.fillAmount = 0;
            }
        }
        // 
        Debug.Log("Power-Up Activated!");

        // 
        collectibles = collectibles- fruitsRequiredForPowerUp;
        CollectiblesText.text = collectibles.ToString();
    }

    void DeactivatePowerUp()
    {
        Player.isPowerUpOn = false;
        Player.playerMoveSpeed -= 4.0f;
    }
}
