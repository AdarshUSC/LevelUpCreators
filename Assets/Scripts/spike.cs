using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the player is tagged as "Player"
        {
            Destroy(other.gameObject); // Destroy the player GameObject
            // You can add additional game over logic or animations here if necessary
        }
    }
}
