using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFloating : MonoBehaviour
{
    float floatTimer;
    void Start()
    {
        floatTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        floatTimer += Time.deltaTime;
        transform.position += (Vector3.up*Time.deltaTime);
        if (floatTimer > 2.0f)
        {
            Destroy(gameObject);
        }

    }
}
