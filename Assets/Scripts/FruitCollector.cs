using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FruitCollector : MonoBehaviour
{
    private int collectibles = 0;
    [SerializeField] private TMP_Text CollectiblesText;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            collectibles++;
            CollectiblesText.text = "Fruits: " + collectibles;
        }
    }
}
