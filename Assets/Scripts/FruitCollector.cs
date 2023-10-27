using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FruitCollector : MonoBehaviour
{
    private int collectibles = 0;
    [SerializeField] private TMP_Text CollectiblesText;

    [SerializeField] private int fruitsRequiredForPowerUp = 5; // 
    [SerializeField] private Button powerUpButton; // 


    private void Start()
    {
        // 
        powerUpButton.interactable=false;
        powerUpButton.onClick.AddListener(ActivatePowerUp); // 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            collectibles++;
            CollectiblesText.text = "Fruits: " + collectibles;
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
        CollectiblesText.text = "Fruits: " + collectibles;
    }
}
