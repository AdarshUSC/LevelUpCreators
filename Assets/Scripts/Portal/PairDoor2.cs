using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // Debug.Log("player color is "+ player.GetComponent<SpriteRenderer>().color);
        // Debug.Log("door color is "+ image.GetComponent<SpriteRenderer>().color);
        if (byTheDoor && image.GetComponent<SpriteRenderer>().color == player.GetComponent<SpriteRenderer>().color && Input.GetKeyUp(KeyCode.UpArrow) && timer>0.1)
        {
            player.transform.position = otherDoor.transform.position;
            byTheDoor = false;
            Debug.Log("From " + transform.position.ToString() + " transfer to " + otherDoor.transform.position.ToString());
            otherDoor.GetComponent<PairDoor1>().timer = 0;

            // GameObject colorPanel = GameObject.FindGameObjectWithTag("CommonCanvas").transform.FindChild("colorPanel").gameObject;
            GameObject mixArea = GameObject.FindGameObjectWithTag("mixArea");
            Color mixAreaColor = mixArea.GetComponent<Image>().color;
            if(mixAreaColor.r==1){
                Player.redCollected-=1;
            } if(mixAreaColor.g==1){
                Player.greenCollected-=1;
            } if(mixAreaColor.b==1){
                Player.blueCollected-=1;
            }
            mixArea.GetComponent<Image>().color = new Color(1,1,1,1);
            //return player back to normal color after exiting the door
            player.GetComponent<SpriteRenderer>().color = Color.white;
            ColorMixer.colorList.Clear();
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
