using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachGoal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Reaching the end");
            EndGame();
        }
    }
    private void EndGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // ÔÚ±à¼­Æ÷ÖÐÍ£Ö¹²¥·Å
        #else
            Application.Quit();
        #endif
    }
}
