using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PairDoor2 : MonoBehaviour
{
    [SerializeReference] GameObject otherDoor;
    [SerializeReference] GameObject player;
    [SerializeReference] GameObject image;
    [SerializeReference] GameObject bg;
    [SerializeReference] GameObject mCamera;
    private Button resetButton;
    public static bool byTheDoor = false;
    public float timer = 0;
    [SerializeField] public GameObject floatingText;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        resetButton = GameObject.FindGameObjectWithTag("ColorReset").GetComponent<Button>();
        mCamera = GameObject.FindGameObjectWithTag("MainCamera");
        // floatingText = GameObject.FindGameObjectWithTag("floatingText");
        byTheDoor = false;
        Color curColor = image.GetComponent<SpriteRenderer>().color;
        image.GetComponent<SpriteRenderer>().color = new Color(curColor.r, curColor.g, curColor.b, 0.5f);
        Color bgColor = bg.GetComponent<SpriteRenderer>().color;
        bg.GetComponent<SpriteRenderer>().color = new Color(bgColor.r, bgColor.g, bgColor.b, 0f);
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
            Color curColor = image.GetComponent<SpriteRenderer>().color;
            image.GetComponent<SpriteRenderer>().color = new Color(curColor.r, curColor.g, curColor.b, 1f);
            Color bgColor = bg.GetComponent<SpriteRenderer>().color;
            bg.GetComponent<SpriteRenderer>().color = new Color(bgColor.r, bgColor.g, bgColor.b, 1f);

            byTheDoor = true;
            resetButton.interactable=true;
            // Debug.Log("by the door is enabled");
            GameObject colorPanel = GameObject.FindGameObjectWithTag("CommonCanvas").transform.Find("ColorPanel").gameObject;
            Debug.Log("color panel position is "+ colorPanel.transform.position.x+",,,,, "+ colorPanel.transform.position.y);
            Vector3 newPos = mCamera.transform.position - Vector3.up * 12.5f + Vector3.left * 23f;
            // Debug.Log("new position is "+ newPos);
            Instantiate(floatingText, newPos,  Quaternion.identity);
            // resetButton.gameObject.SetActive(true);
            GameObject mixArea = GameObject.FindGameObjectWithTag("mixArea");
            if(mixArea!=null){
                mixArea.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            ColorMixer.colorList.Clear();
            Debug.Log("color list length nnear door 2 is "+ ColorMixer.colorList.Count);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Color curColor = image.GetComponent<SpriteRenderer>().color;
            image.GetComponent<SpriteRenderer>().color = new Color(curColor.r, curColor.g, curColor.b, 0.5f);
            Color bgColor = bg.GetComponent<SpriteRenderer>().color;
            bg.GetComponent<SpriteRenderer>().color = new Color(bgColor.r, bgColor.g, bgColor.b, 0f);

            byTheDoor = false;
            resetButton.interactable=false;
            player.GetComponent<SpriteRenderer>().color = Color.white;
            Debug.Log("by the door is disabled");
            ColorMixer.colorList.Clear();
            GameObject mixArea = GameObject.FindGameObjectWithTag("mixArea");
            if(mixArea!=null){
                mixArea.GetComponent<Image>().color = new Color(1,1,1,1);
            }

            // resetButton.gameObject.SetActive(false);
        }
    }
}
