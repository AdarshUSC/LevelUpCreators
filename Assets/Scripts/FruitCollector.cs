using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FruitCollector : MonoBehaviour
{
    private  int collectibles = 0;
    [SerializeField] private TMP_Text CollectiblesText;

    [SerializeField] private int fruitsRequiredForPowerUp = 5; // 
    [SerializeField] private Button powerUpButton; // 

    private void Start()
    {
        // 
        powerUpButton.interactable=false;
        powerUpButton.onClick.AddListener(ActivatePowerUp); // 
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
            if (collectibles >= fruitsRequiredForPowerUp)
            {
                // 
                powerUpButton.interactable = true;
            }
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
            if (collectibles >= fruitsRequiredForPowerUp)
            {
                // 
                powerUpButton.interactable=true;
            }
        } else if (collision.gameObject.CompareTag("ColorCollectible")){
            
            // Debug.Log("I am on color enter 2D" + collision.gameObject.transform.position.y);
            SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
            Player.current_mechs.Add("Color Collected");
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
        // 
        Debug.Log("Power-Up Activated!");

        // 
        powerUpButton.interactable=false;
        collectibles = collectibles- fruitsRequiredForPowerUp;
        CollectiblesText.text = collectibles.ToString();
    }
}
