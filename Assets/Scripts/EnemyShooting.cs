using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject bullet;
    private GameObject player;
    private GameObject mixArea;
    public bool isDectected;
    private float timer;

    public bool changeColor ;
    // Start is called before the first frame update
    void Start()
    {
        // isDectected = false;
        // changeColor = false;
        player = GameObject.FindGameObjectWithTag("Player");
        mixArea = GameObject.FindGameObjectWithTag("mixArea");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

        if(distance<4 && !compareColors(GetComponent<SpriteRenderer>().color, player.GetComponent<SpriteRenderer>().color)){
            timer+= Time.deltaTime;
            if(timer > 1){
                timer = 0;  
                Shoot(); 
            }
        }
        
        // if(firePoint.position.y<=-1.2f && firePoint.position.y>-3.4f && !isDectected && !compareColors(GetComponent<SpriteRenderer>().color, player.GetComponent<SpriteRenderer>().color)){
        //     Shoot();
        //     isDectected = true;
        // } 
        // if (firePoint.position.y < -3.4f)
        // {
        //     FindObjectOfType<ColorMixer>().OnResetClick();
        // }
        // if (firePoint.position.y<-5.0f){
        //     Destroy(this);
        // }
        
    }

    void Shoot(){
        Instantiate(bullet, firePoint.position, Quaternion.identity);
    }

    private bool compareColors(Color color1,Color color2)
    {
        return Mathf.Abs(color1.r-color2.r)<0.005 & 
                Mathf.Abs(color1.g-color2.g)<0.005 & 
                Mathf.Abs(color1.b - color2.b) < 0.005;
    }
}
