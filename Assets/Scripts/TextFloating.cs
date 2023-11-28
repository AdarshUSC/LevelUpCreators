using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFloating : MonoBehaviour
{
    float floatTimer;
    [SerializeReference] GameObject player;
    void Start()
    {
        floatTimer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        floatTimer += Time.deltaTime;
        Vector3 newPos = player.transform.position - Vector3.up*13 + Vector3.left*23 + (Vector3.up* floatTimer * 1.5f) + Vector3.back * 10;
        transform.position = Vector3.Slerp(transform.position, newPos, 2f * Time.deltaTime);
        if (floatTimer > 2.0f)
        {
            Destroy(gameObject);
        }

    }
}
