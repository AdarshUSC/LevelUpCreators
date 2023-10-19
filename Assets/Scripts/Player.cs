using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    float moveSpeed = 3.0f;
    float jumpForce = 3.0f;
    private float Timetaken = 7.0f;
    private int mech1 = 3;
    private int mech2 = 6;
    private int mech3 = 6;
    private float checkpoint1 = 3.0f;
    private float checkpoint2 = 5.0f;
    private float checkpoint3 = 7.0f;
    private float time_running = 0.0f;
    private float[] checks;
    private int count = 0;
    private Boomerang boomerang;
    public GameObject boomerangObject;


    private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdHKBSGlrH4LG-W3gfj3Dc--PUgpnOvAnQwZ1SXpbi_AFyVKQ/formResponse";

    Rigidbody2D rb;
    bool isGrounded = false;
    //private Vector3 respawnPoint;
    public Vector3 respawnPoint;
    public GameObject fallDetector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        time_running = 0.0f;
        count = 0;
        checks = new float[3];
        boomerang = boomerangObject.GetComponent<Boomerang>();
    }

    // Update is called once per frame
    void Update()
    {
        time_running += Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
        }

        CheckGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            // Need to change the direction of the boomerang
            boomerang.Throw(Vector2.up);
        }
    }

    void CheckGrounded()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y / 2 + 0.1f);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Maze")
        {
            Debug.Log("Hit the wall");
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            }
            if(Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, moveSpeed*Time.deltaTime, 0);
            }

        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FallDetection")
        {
            transform.position = respawnPoint;
            Timetaken = time_running;
            Send();
        }
        if (collision.gameObject.tag == "CheckPoint")
        {
            respawnPoint = transform.position;
            Debug.Log("Checkpoint encountered at" + transform.position.x + " " + transform.position.y);
            checks[count] = time_running;
            count++;
           
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject); 
            this.transform.position = this.respawnPoint;  
        }
    }
    public void Send()
    {
        Debug.Log("Send called");
        StartCoroutine(Post(Timetaken, mech1, mech2, mech3, checkpoint1, checkpoint2, checkpoint3));
    }

    IEnumerator Post(float timetaken, int mech1, int mech2, int mech3, float cp1, float cp2, float cp3)
    {
        Debug.Log("Post called"+ (string.Format("{0:N2}", Timetaken))+" "+(string.Format("{0}", mech1)));
        WWWForm form = new WWWForm();
        form.AddField("entry.1747016377", string.Format("{0:N2}", Timetaken));
        form.AddField("entry.305553560", string.Format("{0}", mech1));
        form.AddField("entry.1168732002", string.Format("{0}", mech2));
        form.AddField("entry.1274496277", string.Format("{0}", mech3));
        form.AddField("entry.1477920271", string.Format("{0:N2}", checks[0]));
        form.AddField("entry.2118230736", string.Format("{0:N2}", checks[1]-checks[0]));
        form.AddField("entry.2104200455", string.Format("{0:N2}", checks[2]-checks[1]));
        UnityWebRequest WWW = UnityWebRequest.Post(URL, form);
        yield return WWW.SendWebRequest();

    }


}
