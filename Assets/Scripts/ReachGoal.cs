using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachGoal : MonoBehaviour
{
    [SerializeReference] GameObject winCanvas;

    private void Start()
    {
        winCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Reaching the end");
            winCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
