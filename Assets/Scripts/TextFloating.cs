using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFloating : MonoBehaviour
{
    float floatTimer;
    [SerializeReference] GameObject mCamera;
    void Start()
    {
        floatTimer = 0.0f;
        mCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        floatTimer += Time.deltaTime;
        Vector3 newPos = mCamera.transform.position - Vector3.up*12.5f + Vector3.left*23 + (Vector3.up* floatTimer * 1f) + Vector3.forward * 10f;
        //transform.position = Vector3.Slerp(transform.position, newPos, 2f * Time.deltaTime);
        transform.position = newPos;
        if (floatTimer > 1.0f)
        {
            Destroy(gameObject);
        }

    }
}
