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
        if (other.CompareTag("Player"))
        {
           
            Vector3 respawnPos = player.respawnPoint;

            other.transform.position = respawnPos;

        }
    }
}
