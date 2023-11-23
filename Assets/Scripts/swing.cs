using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swing : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float angle = 10.0f;

    private float currentAngle = 0;
    private float timer = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * speed;
        float angle = Mathf.Sin(timer) * this.angle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + currentAngle));
    }
}
