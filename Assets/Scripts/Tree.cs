using UnityEngine;

public class Tree : MonoBehaviour
{
    // Public variable to store the tree's fruits
    public GameObject[] fruits;
    private float greenTimer;
    private bool greenOn;

    GameObject player;

    void Start()
    {
        // Disable all the fruits initially
        foreach (var fruit in fruits)
        {
            fruit.SetActive(false);
        }

        greenOn=false;
        greenTimer=0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(greenOn){
            greenTimer+= Time.deltaTime;
            Debug.Log("green timer is "+ greenTimer);
            if(greenTimer > 5.0f){
                // foreach(Transform transform in colorPanel.transform) {
                //     if(transform.CompareTag("ColorButton")) {
                //         Button colorButton = transform.gameObject.GetComponent<Button>();
                //         colorButton.interactable=false; // switching off the colors when power up is being used
                //     }
                // }
                greenOn=false;
                greenTimer = 0.0f;
                // GameObject.FindGameObjectWithTag("mixArea").GetComponent<Button>().interactable=false;
                Player.playerMoveSpeed=8.0f;
                player.GetComponent<SpriteRenderer>().color = Color.white;
                return;
            } 
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        Color currColor =  collision.gameObject.GetComponent<SpriteRenderer>()!=null?collision.gameObject.GetComponent<SpriteRenderer>().color:Color.white;
        if (collision.gameObject.CompareTag("Boomerang") && currColor == Color.green){
            player.GetComponent<SpriteRenderer>().color = Color.green;
            greenOn=true;
            greenTimer=Time.deltaTime;
            Player.playerMoveSpeed=1.0f;
            transform.position = transform.parent.position;
            gameObject.SetActive(false);
        } 
    }

    public void DropFruits()
    {
        // This method is called when the tree is hit by the boomerang

        // Enable all the fruits to make them fall
        foreach (var fruit in fruits)
        {
            if (fruit != null)
            {
                fruit.SetActive(true);
            }
        }
    }
}