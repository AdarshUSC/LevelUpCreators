using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachGoal : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
