using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] public  float timelimit = 90.0f;
    public Player player;
    float limit = 0;
    bool once = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        once = true;
        player.timelimit = timelimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (timelimit > 5)
        {
            timelimit -= Time.deltaTime;

        }
        else if (timelimit <= 5 && timelimit > 0)
        {
            timerText.color = Color.red;
            timelimit -= Time.deltaTime;
        }
        else
        {
            timelimit = 0;
            GameOver();
            
        }
        int minutes = Mathf.FloorToInt(timelimit / 60);
        int seconds = Mathf.FloorToInt(timelimit % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
      //  timerText.text = string.Format("{0:00}", seconds);
    }
    void GameOver()
    {   //if (player.number_of_lives < 0)
        //    timelimit = 0;
        //else
        {   if(player !=null && player.transform != null)
            Player.deathPoints.Add(player.transform.position);
            Player.death_reasons.Add("Timeout");
            if(player.number_of_lives>0)
                 player.number_of_lives = 0;
            player.lives[1].enabled = false;
            player.lives[0].enabled = false;
            player.lives[2].enabled = false;
            player.LoseLife();
            //once = false;
            player.transform.position = player.respawnPoint;
            timerText.color = Color.white;
            timelimit = 0;
            //timelimit = player.timelimit;
        }
    }
}


