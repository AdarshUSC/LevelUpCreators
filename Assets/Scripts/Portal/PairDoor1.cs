using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PairDoor1 : MonoBehaviour
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
        if (byTheDoor && image.GetComponent<SpriteRenderer>().color == player.GetComponent<SpriteRenderer>().color && Input.GetKeyUp(KeyCode.UpArrow) && timer > 0.1)
        {
            player.transform.position = otherDoor.transform.position;
            byTheDoor = false;
            Debug.Log("From " + transform.position.ToString() + " transfer to " + otherDoor.transform.position.ToString());
            otherDoor.GetComponent<PairDoor2>().timer = 0;
            GameObject[] colorButtons = GameObject.FindGameObjectsWithTag("ColorButton");
            foreach(GameObject colorButton in colorButtons){
                Button button = colorButton.GetComponent<Button>();
                if(button.interactable==false){
                    Color curr = button.GetComponent<Image>().color;
                    if(curr==Color.red){
                        Player.redCollected--;
                    } if(curr==Color.blue){
                        Player.blueCollected--;
                    } if(curr==Color.green){
                        Player.greenCollected--;
                    }
                }
            }

            //return player back to normal color after exiting the door?
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
