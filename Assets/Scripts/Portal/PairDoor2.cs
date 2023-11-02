using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairDoor2 : MonoBehaviour
{
    [SerializeReference] GameObject otherDoor;
    [SerializeReference] GameObject player;
    [SerializeReference] GameObject image;

    public static bool byTheDoor = false;
    public float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        byTheDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (byTheDoor && image.GetComponent<SpriteRenderer>().color == player.GetComponent<SpriteRenderer>().color && Input.GetKeyUp(KeyCode.UpArrow) && timer>0.1)
        {
            player.transform.position = otherDoor.transform.position;
            byTheDoor = false;
            Debug.Log("From " + transform.position.ToString() + " transfer to " + otherDoor.transform.position.ToString());
            otherDoor.GetComponent<PairDoor1>().timer = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            byTheDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            byTheDoor = false;
        }
    }
}
