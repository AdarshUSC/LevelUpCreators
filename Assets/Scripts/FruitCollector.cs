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
            Destroy(collision.gameObject);
            collectibles++;
            CollectiblesText.text = collectibles.ToString();
            //
            if (collectibles >= fruitsRequiredForPowerUp)
            {
                // 
                powerUpButton.interactable = true;
            }
        } else if (collision.gameObject.CompareTag("ColorCollectible")){
            
            Debug.Log("I am on color enter 2D" + collision.gameObject.transform.position.y);
            SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
            Destroy(collision.gameObject);
            if(sr.color==Color.red){
                if(Player.redCollected++==0){
                    GameObject[] colorButtons = GameObject.FindGameObjectsWithTag("ColorButton");
                    foreach(GameObject colorButton in colorButtons){
                        Button button = colorButton.GetComponent<Button>();
                        Color curr = button.GetComponent<Image>().color;
                        if(curr==Color.red){
                            button.interactable=true;
                            break;
                        }
                    }
                }
            } else if(sr.color==Color.green){
                if(Player.greenCollected++==0){
                    GameObject[] colorButtons = GameObject.FindGameObjectsWithTag("ColorButton");
                    foreach(GameObject colorButton in colorButtons){
                        Button button = colorButton.GetComponent<Button>();
                        Color curr = button.GetComponent<Image>().color;
                        if(curr==Color.green){
                            button.interactable=true;
                            break;
                        }
                    }
                }
            } else if(sr.color==Color.blue){
                if(Player.blueCollected++==0){
                    GameObject[] colorButtons = GameObject.FindGameObjectsWithTag("ColorButton");
                    foreach(GameObject colorButton in colorButtons){
                        Button button = colorButton.GetComponent<Button>();
                        Color curr = button.GetComponent<Image>().color;
                        if(curr==Color.blue){
                            button.interactable=true;
                            break;
                        }
                    }
                }
            }

            // Player.CollectablePoints.Add(collision.gameObject.transform.position);
            // Destroy(collision.gameObject);
            // CollectiblesText.text = collectibles.ToString();
            Debug.Log("Colors Collected :"+ Player.redCollected+","+Player.greenCollected+","+Player.blueCollected);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Debug.Log("I am on trigger enter 2D"+ collision.gameObject.transform.position.y);
            Player.CollectablePoints.Add(collision.gameObject.transform.position);
            Destroy(collision.gameObject);
            collectibles++;
            CollectiblesText.text = collectibles.ToString();
            //
            if (collectibles >= fruitsRequiredForPowerUp)
            {
                // 
                powerUpButton.interactable=true;
            }
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
