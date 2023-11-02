using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairDoors : MonoBehaviour
{
    [SerializeReference] GameObject otherDoor;
    [SerializeReference] GameObject player;
    [SerializeReference] GameObject image;

    private bool byTheDoor = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (byTheDoor && image.GetComponent<SpriteRenderer>().color == player.GetComponent<SpriteRenderer>().color && Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.transform.position = otherDoor.transform.position;
            byTheDoor = false;
            Debug.Log(otherDoor.transform.position);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            byTheDoor = true;
        }
        else byTheDoor = false;
    }
}
