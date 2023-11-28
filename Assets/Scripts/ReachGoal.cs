using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReachGoal : MonoBehaviour
{
    [SerializeReference] GameObject winCanvas;
    [SerializeReference] GameObject player;
    [SerializeReference] GameObject winTxt;

    private float timer = 0;

    private void Start()
    {
        winCanvas.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Reaching the end");
            int minutes = (int)timer / 60;
            int seconds = (int)timer % 60;
            winCanvas.SetActive(true);
            winTxt.GetComponent<TMP_Text>().text = "Congratulations!\nYou used " + string.Format("{0:00}:{1:00}", minutes, seconds);
            Time.timeScale = 0f;
            timer = 0;
        }
    }
}
