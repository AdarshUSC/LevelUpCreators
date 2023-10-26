using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Player p;
    private Rigidbody2D rb;
    public float force;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        force = 10.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity  = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot+90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // void OnTriggerEnter2D(Collider2D other){

    //     if(other.gameObject.CompareTag("Player")){
    //         other.gameObject.transform.position = p.respawnPoint;
    //        // Destroy(gameObject);
    //     }
    //     // Destroy(gameObject);
    // }
}
