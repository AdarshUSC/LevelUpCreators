using UnityEngine;

public class Spike : MonoBehaviour
{
    private Player player; 

    private void Start()
    {
      
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !Player.isPowerUpOn)
        {
           
            Vector3 respawnPos = player.respawnPoint;
            Player.deathPoints.Add(other.transform.position);
            if (player.number_of_lives >= 1)
            {
                other.transform.position = respawnPos;
                player.number_of_lives--;
                player.LoseLife();
            }
            else
            {
                player.Lost();
            }
            other.transform.position = respawnPos;

        }
    }
}
