using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    // Start is called before the first frame update
    float floatTimer;
    void Start()
    {
        floatTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        floatTimer+=Time.deltaTime;
        if(floatTimer>2.0f){
            Destroy(gameObject);
        }

    }
}
