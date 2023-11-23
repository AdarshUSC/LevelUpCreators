using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vine : MonoBehaviour
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
            Player.death_reasons.Add("Vines");

            other.transform.position = respawnPos;

            player.LoseLife();


            other.transform.position = respawnPos;

        }
    }
}
