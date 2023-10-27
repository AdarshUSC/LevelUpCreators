using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static float playerMoveSpeed = 5.0f;

    public static bool isPowerUpOn = false;
    float jumpForce = 8.0f;
    private float Timetaken = 7.0f;
    public static int antigravity = 0;
    public static int reflection = 0;
    public static int camouflage = 0;
    private float checkpoint1 = 0.0f;
    private float checkpoint2 = 0.0f;
    private float checkpoint3 = 0.0f;
    private float checkpoint4 = 0.0f;
    private float time_running = 0.0f;
    private float time_checkpoint = 0.0f;
    private float[] checks;
    private int count = 0;
    private Boomerang boomerang;
    public GameObject boomerangObject;
    [HideInInspector]
    public bool isFacingRight;
    public float inputHorizontal;
    public float inputVertical;
    private Transform playerTransform;
    private Vector3 originalScale;
    private bool ogscale;


    private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdHKBSGlrH4LG-W3gfj3Dc--PUgpnOvAnQwZ1SXpbi_AFyVKQ/formResponse";

    Rigidbody2D rb;
    //private Vector3 respawnPoint;
    public Vector3 respawnPoint;
    public Vector3 initialPosition;
    public GameObject fallDetector;
    public LayerMask groundLayer;

    [Header("Collision")]
    bool onGround = false;
    public float groundLine = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        initialPosition = transform.position;
        time_running = 0.0f;
        time_checkpoint = 0.0f;
        count = 0;
        checks = new float[3];
        boomerang = boomerangObject.GetComponent<Boomerang>();
        playerTransform = GetComponent<Transform>();
        originalScale = playerTransform.localScale;
        ogscale = true;
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        time_running += Time.deltaTime;
        time_checkpoint += Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-playerMoveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(playerMoveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -playerMoveSpeed * Time.deltaTime, 0);
        }

        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundLine, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            // Need to change the direction of the boomerang
            boomerang.Throw(Vector2.up);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ogscale)
            {
                Vector3 newScale = new Vector3(5.0f, 5.0f, 5.0f);
                playerTransform.localScale = newScale;
                ogscale = false;
            }
            else
            {
                playerTransform.localScale = originalScale;
                ogscale = true;
            }
        }
    }
    private void FixedUpdate()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (inputHorizontal<0 & isFacingRight)
        {
            Flip();
        }
        if (inputHorizontal > 0 & !isFacingRight)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Maze")
        {
            Debug.Log("Hit the wall");
            if (Input.GetKey(KeyCode.LeftArrow) )
            {
                transform.Translate(playerMoveSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(-playerMoveSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, -playerMoveSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, playerMoveSpeed * Time.deltaTime, 0);
            }

        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FallDetection")
        {
            transform.position = initialPosition;
            Timetaken = time_running;
            Send();
        }
        if (collision.gameObject.tag == "Checkpoint1")
        {
            respawnPoint = transform.position;
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint1 = time_checkpoint;
            time_checkpoint = 0.0f;
        }
        if (collision.gameObject.tag == "Checkpoint2")
        {
            respawnPoint = transform.position;
            Debug.Log("Checkpoint encountered at" + transform.position.x + " " + transform.position.y + " ");
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint2 = time_checkpoint;
            time_checkpoint = 0.0f;
        }
        if (collision.gameObject.tag == "Checkpoint3")
        {
            respawnPoint = transform.position;
            Debug.Log("Checkpoint encountered at" + transform.position.x + " " + transform.position.y);
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint3 = time_checkpoint;
            time_checkpoint = 0.0f;
        }
        if (collision.gameObject.tag == "Checkpoint4")
        {
            respawnPoint = transform.position;
            Debug.Log("Checkpoint encountered at" + transform.position.x + " " + transform.position.y);
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint4 = time_checkpoint;
            time_checkpoint = 0.0f;
        }
        if (collision.gameObject.tag == "Bullet" && !isPowerUpOn)
        {
            Destroy(collision.gameObject);
            this.transform.position = this.respawnPoint;
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1;
        }
    }
    public void Flip()
    {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }
    public void Send()
    {
        Debug.Log("Send called");
        StartCoroutine(Post(Timetaken, antigravity, reflection, camouflage, checkpoint1, checkpoint2, checkpoint3, checkpoint4));
    }

    IEnumerator Post(float timetaken, int mech1, int mech2, int mech3, float cp1, float cp2, float cp3, float cp4)
    {
        Debug.Log("Post called" + (string.Format("{0:N2}", Timetaken)) + " " + (string.Format("{0}", mech1)));
        WWWForm form = new WWWForm();
        form.AddField("entry.1747016377", string.Format("{0:N2}", Timetaken));
        form.AddField("entry.305553560", string.Format("{0}", mech1));
        form.AddField("entry.1168732002", string.Format("{0}", mech2));
        form.AddField("entry.1274496277", string.Format("{0}", mech3));
        form.AddField("entry.1477920271", string.Format("{0:N2}", cp1));
        form.AddField("entry.2118230736", string.Format("{0:N2}", cp2));
        form.AddField("entry.2104200455", string.Format("{0:N2}", cp3));
        form.AddField("entry.1640424427", string.Format("{0:N2}", cp4));
        UnityWebRequest WWW = UnityWebRequest.Post(URL, form);
        yield return WWW.SendWebRequest();
    }


}
