using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] public float timelimit;
    float time;
    void Start()
    {

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
        timerText.text = string.Format("{0:00}", seconds);
    }
    void GameOver()
    {
        timerText.text = "Game Over";
        //need to implement
    }
}


