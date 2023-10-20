using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject bullet;
    private GameObject player;
    private float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance<3 && !compareColors(GetComponent<SpriteRenderer>().color, player.GetComponent<SpriteRenderer>().color)){
            timer+= Time.deltaTime;
            if(timer > 1){
                timer = 0;  
                Shoot(); 
            }
        }      
    }

    void Shoot(){
        Instantiate(bullet, firePoint.position, Quaternion.identity);
    }

    private bool compareColors(Color color1,Color color2)
    {
        return (Mathf.Abs(color1.r-color2.r) < 0.005 & 
                Mathf.Abs(color1.g-color2.g) < 0.005 & 
                Mathf.Abs(color1.b - color2.b) < 0.005);
    }
}
