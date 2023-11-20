using UnityEngine;

public class SensorScript : MonoBehaviour
{
    public bool isInWall = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision.name);
        if (collision.tag == "Maze")
        {
            isInWall = true;
            Debug.Log(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Maze") isInWall = false;
    }
}
