using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PairDoor1 : MonoBehaviour
{
    [SerializeReference] GameObject otherDoor;
    [SerializeReference] GameObject player;
    [SerializeReference] GameObject image;
    [SerializeReference] GameObject bg;
    [SerializeReference] GameObject mCamera;
    private Button resetButton;
    private GameObject mixArea;
    public static bool byTheDoor = false;
    public float timer = 0;
    [SerializeField] public GameObject floatingText;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        resetButton = GameObject.FindGameObjectWithTag("ColorReset").GetComponent<Button>();
        mixArea = GameObject.FindGameObjectWithTag("mixArea");
        mCamera = GameObject.FindGameObjectWithTag("MainCamera");
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
        if (byTheDoor && image.GetComponent<SpriteRenderer>().color == player.GetComponent<SpriteRenderer>().color && Input.GetKeyUp(KeyCode.UpArrow) && timer > 0.1)
        {
            player.transform.position = otherDoor.transform.position;
            byTheDoor = false;
            Debug.Log("From " + transform.position.ToString() + " transfer to " + otherDoor.transform.position.ToString());
            otherDoor.GetComponent<PairDoor2>().timer = 0;
            Player.current_mechs.Add("Portal");
            // GameObject colorPanel = GameObject.FindGameObjectWithTag("CommonCanvas").transform.FindChild("colorPanel").gameObject;
            GameObject mixArea = GameObject.FindGameObjectWithTag("mixArea");
            Color mixAreaColor = mixArea.GetComponent<Image>().color;
            if(mixAreaColor.r==1){
                Debug.Log("hello red");
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
            // hightlight code
            Color curColor = image.GetComponent<SpriteRenderer>().color;
            image.GetComponent<SpriteRenderer>().color = new Color(curColor.r, curColor.g, curColor.b, 1f);
            Color bgColor = bg.GetComponent<SpriteRenderer>().color;
            bg.GetComponent<SpriteRenderer>().color = new Color(bgColor.r, bgColor.g, bgColor.b, 1f);

            byTheDoor = true;
            // resetButton.gameObject.SetActive(true);
            Debug.Log("by the door is enabled");
            resetButton.interactable=true;
            GameObject colorPanel = GameObject.FindGameObjectWithTag("CommonCanvas").transform.Find("ColorPanel").gameObject;
            Debug.Log("color panel position before is "+ colorPanel.transform.position.x+",,,,, "+ colorPanel.transform.position.y+ ",,,,,,,"+(colorPanel.transform.position.y+90));
            Vector3 newPos = mCamera.transform.position - Vector3.up * 12.5f + Vector3.left * 23f;
            // Debug.Log("new position is "+ newPos);
            Instantiate(floatingText, newPos,  Quaternion.identity);            
            // Debug.Log("color panel position after is "+ colorPanel.transform.position.x+",,,,, "+ colorPanel.transform.position.y+ ",,,,,,,"+(colorPanel.transform.position.y+90));
            if(mixArea!=null){
                mixArea.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            ColorMixer.colorList.Clear();
            Debug.Log("color list length nnear door 1 is "+ ColorMixer.colorList.Count);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            // hightlight code
            Color curColor = image.GetComponent<SpriteRenderer>().color;
            image.GetComponent<SpriteRenderer>().color = new Color(curColor.r, curColor.g, curColor.b, 0.5f);
            Color bgColor = bg.GetComponent<SpriteRenderer>().color;
            bg.GetComponent<SpriteRenderer>().color = new Color(bgColor.r, bgColor.g, bgColor.b, 0f);


            byTheDoor = false;
            resetButton.interactable=false;
            player.GetComponent<SpriteRenderer>().color = Color.white;
            // Debug.Log("by the door is disabled");
            ColorMixer.colorList.Clear();
            if(mixArea!=null){
                mixArea.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            // resetButton.gameObject.SetActive(false);
        }
    }
}
