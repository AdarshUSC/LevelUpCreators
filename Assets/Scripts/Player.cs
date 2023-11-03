using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;


public class Player : MonoBehaviour
{
    public static float playerMoveSpeed = 8.0f;

    public static bool isPowerUpOn = false;
    float jumpForce = 8.0f;
    private float Timetaken = 7.0f;
    public static int antigravity = 0;
    public static int reflection = 0;
    public static int camouflage = 0;
    public static int resize = 0;
    public static int boomerang_used = 0;
    public static int powerup = 0;
    public static int collectables = 0;

    public int number_of_lives = 3;
    private float[][] checkpoints;

    private float time_running = 0.0f;
    private float time_checkpoint = 0.0f;
    public float timelimit = 90.0f;
    private float[] checks;
    private int count = 0;
    private Boomerang boomerang;
    public GameObject boomerangObject;
    [HideInInspector]
    public bool isFacingRight;
    public float inputHorizontal;
    public float inputVertical;
    private Transform playerTransform;
    //private Vector3 originalScale;
    //private bool ogscale;
    public static List<Vector2> deathPoints;
    public static List<Vector2> CollectablePoints;
    public static List<string> death_reasons;
    public static List<string> mechanics_cp1;
    public static List<string> mechanics_cp2;
    public static List<string> mechanics_cp3;
    public static List<string> mechanics_cp4;
    public static List<string> mechanics_cp5;
    public static List<string> mechanics_cp6;
    public static List<string> mechanics_cp7;
    public static List<string> mechanics_cp8;
    public static List<string> mechanics_cp9;
    public static List<string> mechanics_exit;
    public static List<string> current_mechs;
    private List<float> checkpoint1;
    private List<float> checkpoint2;
    private List<float> checkpoint3;
    private List<float> checkpoint4;
    private List<float> checkpoint5;
    private List<float> checkpoint6;
    private List<float> checkpoint7;
    private List<float> checkpoint8;
    private List<float> checkpoint9;

    System.String current_sublevel = "";

    [SerializeReference] GameObject lostCanvas;
    public Image[] lives;
    public int win = 0;

    public float upBoundary;
    public float downBoundary;

    private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdHKBSGlrH4LG-W3gfj3Dc--PUgpnOvAnQwZ1SXpbi_AFyVKQ/formResponse";

    Rigidbody2D rb;
    //private Vector3 respawnPoint;
    public Vector3 respawnPoint;
    public Vector3 initialPosition;
    public GameObject fallDetector;
    public LayerMask groundLayer;
    private Vector3 direction;

    [Header("Collision")]
    bool onGround = false;
    public float groundLine = 2;

    public static int redCollected = 5;
    public static int blueCollected = 5;
    public static int greenCollected = 5;
    public static int redUsed = 0;
    public static int blueUsed = 0;
    public static int greenUsed = 0;
    private TrailRenderer trail;


    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;  // Start with the trail turned off.

        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        initialPosition = transform.position;
        number_of_lives = 2;
        count = 0;
        win = 0;
        current_sublevel = "entry";
        boomerang = boomerangObject.GetComponent<Boomerang>();
        playerTransform = GetComponent<Transform>();
        //originalScale = playerTransform.localScale;
        //ogscale = true;
        isFacingRight = true;
        timelimit = 90.0f;
        current_mechs = new List<string>();
        death_reasons = new List<string>();
        deathPoints = new List<Vector2>();
        CollectablePoints = new List<Vector2>();
        direction = new Vector2(1, 0);
        lostCanvas.SetActive(false);
        checkpoints = new float[8][];
        checkpoint1 = new List<float>();
        checkpoint2 = new List<float>();
        checkpoint3 = new List<float>();
        checkpoint4 = new List<float>();
        checkpoint5 = new List<float>();
        checkpoint6 = new List<float>();
        checkpoint7 = new List<float>();
        checkpoint8 = new List<float>();
        checkpoint9 = new List<float>();

        playerMoveSpeed = 8.0f;

    }

    // Update is called once per frame
    void Update()
    {
        checkBoundary();
        //Debug.Log(gameObject.GetComponent<Collider2D>().bounds.size);
        groundLine = (float)(gameObject.GetComponent<Collider2D>().bounds.size.y * 1.4 / 2.4);
        time_running += Time.deltaTime;
        time_checkpoint += Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-playerMoveSpeed * Time.deltaTime, 0, 0);
            direction = Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(playerMoveSpeed * Time.deltaTime, 0, 0);
            direction = Vector2.right;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -playerMoveSpeed * Time.deltaTime, 0);
        }

        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundLine, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {

            float actualJumpForce = isPowerUpOn ? jumpForce * 2.0f : jumpForce;

            rb.velocity = new Vector2(rb.velocity.x, actualJumpForce);
 

        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            boomerang_used++;
            current_mechs.Add("Boomerang");
            boomerang.GetComponent<SpriteRenderer>().color = GameObject.FindGameObjectWithTag("mixArea").GetComponent<Image>().color;
            Color currColor = boomerang.GetComponent<SpriteRenderer>().color;
            if (currColor == Color.red)
            {
                if (redCollected > 0)
                {
                    boomerang.Throw(direction);
                    Player.redCollected--;
                    redUsed++;
                }
            }
            else if (currColor == Color.green)
            {
                if (greenCollected > 0)
                {
                    boomerang.Throw(direction);
                    Player.greenCollected--;
                    greenUsed++;
                }
            }
            else if (currColor == Color.blue)
            {
                if (blueCollected > 0)
                {
                    boomerang.Throw(direction);
                    Player.blueCollected--;
                    blueUsed++;
                }
            }
        }

        if (isPowerUpOn)
        {
            trail.enabled = true;
        }
        else
        {
            trail.enabled = false;
        }

        /*if (Input.GetKeyDown(KeyCode.R))
        {
            if (ogscale)
            {
                Vector3 newScale = new Vector3(3.0f, 3.0f, 3.0f);
                playerTransform.localScale = newScale;
                ogscale = false;
                resize++;
                Debug.Log("Adding resize" + current_mechs.Count);
                current_mechs.Add("Resize");
            }
            else
            {
                playerTransform.localScale = originalScale;
                ogscale = true;
            }
        }*/
    }
    private void FixedUpdate()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (inputHorizontal < 0)
        {
            isFacingRight = false;
            Flip(isFacingRight);
        }
        if (inputHorizontal > 0)
        {
            isFacingRight = true;
            Flip(isFacingRight);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Maze")
        {
            Debug.Log("Hit the wall");
            if (Input.GetKey(KeyCode.LeftArrow))
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
        //Debug.Log("I am on trigger player");
        if (collision.gameObject.tag == "FallDetection")
        {
            // transform.position = initialPosition;
            Timetaken = time_running;
            Debug.Log("mc" + current_mechs.Count);
            mechanics_exit = current_mechs;
            current_mechs = new List<string>();
            win = 1;
            Send();

        }
        if (collision.gameObject.tag == "Checkpoint1")
        {
            respawnPoint.x = transform.position.x + 4;
            respawnPoint.y = transform.position.y;
            Debug.Log("enter cp1");
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint1.Add(time_checkpoint);
            time_checkpoint = 0.0f;
            timelimit = 270.0f;
            Debug.Log("mc" + current_mechs.Count);
            if(mechanics_cp1!=null)
            mechanics_cp1.AddRange(current_mechs);
            else
            mechanics_cp1 = current_mechs;
            current_mechs = new List<string>();
           
        }
        if (collision.gameObject.tag == "Checkpoint2")
        {
            respawnPoint.x = transform.position.x + 4;
            respawnPoint.y = transform.position.y;
            Debug.Log("Checkpoint encountered at" + transform.position.x + " " + transform.position.y + " ");
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint2.Add(time_checkpoint);
            time_checkpoint = 0.0f;
            timelimit = 240.0f;
            Debug.Log("mc" + current_mechs.Count);
            if (mechanics_cp2 != null)
                mechanics_cp2.AddRange(current_mechs);
            else
                mechanics_cp2 = current_mechs;
            current_mechs = new List<string>();
        }
        if (collision.gameObject.tag == "Checkpoint3")
        {
            respawnPoint.x = transform.position.x + 4;
            respawnPoint.y = transform.position.y;
            Debug.Log("Checkpoint encountered at" + transform.position.x + " " + transform.position.y);
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint3.Add(time_checkpoint);
            time_checkpoint = 0.0f;
            timelimit = 210.0f;
            if (mechanics_cp3 != null)
                mechanics_cp3.AddRange(current_mechs);
            else
                mechanics_cp3 = current_mechs;
            current_mechs = new List<string>();
           
        }
        if (collision.gameObject.tag == "Checkpoint4")
        {
            respawnPoint.x = transform.position.x + 4;
            respawnPoint.y = transform.position.y;
            Debug.Log("Checkpoint encountered at" + transform.position.x + " " + transform.position.y);
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint4.Add(time_checkpoint);
            time_checkpoint = 0.0f;
            if (mechanics_cp4 != null)
                mechanics_cp4.AddRange(current_mechs);
            else
                mechanics_cp4 = current_mechs;
            current_mechs = new List<string>();
            timelimit = 180.0f;
            
        }
        if (collision.gameObject.tag == "Checkpoint5")
        {
            respawnPoint.x = transform.position.x + 4;
            respawnPoint.y = transform.position.y;
            Debug.Log("Checkpoint encountered at" + transform.position.x + " " + transform.position.y);
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint5.Add(time_checkpoint);
            time_checkpoint = 0.0f;
            if (mechanics_cp5 != null)
                mechanics_cp5.AddRange(current_mechs);
            else
                mechanics_cp5 = current_mechs;
            current_mechs = new List<string>();
            timelimit = 150.0f;
         
        }
        if (collision.gameObject.tag == "Checkpoint6")
        {
            respawnPoint.x = transform.position.x + 4;
            respawnPoint.y = transform.position.y;
            Debug.Log("Checkpoint encountered at" + transform.position.x + " " + transform.position.y);
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint6.Add(time_checkpoint);
            time_checkpoint = 0.0f;
            if (mechanics_cp6 != null)
                mechanics_cp6.AddRange(current_mechs);
            else
                mechanics_cp6 = current_mechs;
            current_mechs = new List<string>();
            timelimit = 120.0f;
           
        }
        if (collision.gameObject.tag == "Checkpoint7")
        {
            respawnPoint.x = transform.position.x + 4;
            respawnPoint.y = transform.position.y;
            Debug.Log("Checkpoint encountered at" + transform.position.x + " " + transform.position.y);
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint7.Add(time_checkpoint);
            time_checkpoint = 0.0f;


            if (mechanics_cp7 != null)
                mechanics_cp7.AddRange(current_mechs);
            else
                mechanics_cp7 = current_mechs;
            current_mechs = new List<string>();
            timelimit = 90.0f;
           
        }
        if (collision.gameObject.tag == "Checkpoint8")
        {
            respawnPoint.x = transform.position.x + 4;
            respawnPoint.y = transform.position.y;
            Debug.Log("Checkpoint encountered at" + transform.position.x + " " + transform.position.y);
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint8.Add(time_checkpoint);
            time_checkpoint = 0.0f;
            if (mechanics_cp8 != null)
                mechanics_cp8.AddRange(current_mechs);
            else
                mechanics_cp8 = current_mechs;
            current_mechs = new List<string>();
            timelimit = 60.0f;
            
        }
        if (collision.gameObject.tag == "Checkpoint9")
        {
            respawnPoint.x = transform.position.x + 4;
            respawnPoint.y = transform.position.y;
            Debug.Log("Checkpoint encountered at" + transform.position.x + " " + transform.position.y);
            Debug.Log("Time taken" + time_checkpoint);
            checkpoint9.Add(time_checkpoint);
            time_checkpoint = 0.0f;
            if (mechanics_cp9 != null)
                mechanics_cp9.AddRange(current_mechs);
            else
                mechanics_cp9 = current_mechs;
            current_mechs = new List<string>();
            timelimit = 30.0f;
           
        }
        if (collision.gameObject.tag == "Bullet" && !isPowerUpOn)
        {
            Destroy(collision.gameObject);
            deathPoints.Add(this.transform.position);
            death_reasons.Add("Color");

            this.transform.position = this.respawnPoint;
            LoseLife();

            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1;
        }
    }
    public void Flip(bool isFacingRight)
    {
        Vector2 currentScale = gameObject.transform.localScale;
        if (isFacingRight)
            currentScale.x = Mathf.Abs(currentScale.x);
        else currentScale.x = -Mathf.Abs(currentScale.x);
        gameObject.transform.localScale = currentScale;
    }
    public void Send()
    {
        Debug.Log("Send called");
        Debug.Log("Collectable locations" + ConvertVectorListToString(CollectablePoints));
        Debug.Log("cp1");
        StartCoroutine(Post(Timetaken, antigravity, reflection, camouflage, resize, powerup, boomerang_used, CollectablePoints.Count, win, number_of_lives));
    }

    IEnumerator Post(float timetaken, int mech1, int mech2, int mech3, int mech4, int mech5, int mech6,int score, int win, int lives)
    {
        Debug.Log("Post called" + (string.Format("{0:N2}", Timetaken)) + " " + (string.Format("{0}", mech1)));
        WWWForm form = new WWWForm();
        form.AddField("entry.1747016377", string.Format("{0:N2}", Timetaken));
        form.AddField("entry.305553560", string.Format("{0}", mech1));
        form.AddField("entry.1168732002", string.Format("{0}", mech2));
        form.AddField("entry.1274496277", string.Format("{0}", mech3));
        form.AddField("entry.1477920271", string.Join(", ", checkpoint1.Select(f => f.ToString())) + "*" + ConvertListToString(mechanics_cp1));
        form.AddField("entry.2118230736", string.Join(", ", checkpoint2.Select(f => f.ToString())) + "*" + ConvertListToString(mechanics_cp2));
        form.AddField("entry.2104200455", string.Join(", ", checkpoint3.Select(f => f.ToString())) + "*" + ConvertListToString(mechanics_cp3));
        form.AddField("entry.1640424427", string.Join(", ", checkpoint4.Select(f => f.ToString())) + "*" + ConvertListToString(mechanics_cp4));
        form.AddField("entry.73026754", ConvertVectorListToString(deathPoints));
        form.AddField("entry.1506435532", SceneManager.GetActiveScene().name);
        form.AddField("entry.1784470240", string.Format("{0}", mech4));
        form.AddField("entry.767592848", string.Format("{0}", mech5));
        form.AddField("entry.161259885", string.Format("{0}", mech6));
        form.AddField("entry.1830111561", string.Format("{0}", win));
        form.AddField("entry.103162259", string.Format("{0}", lives));
        form.AddField("entry.1934928186", string.Format("{0}", score));
        // form.AddField("entry.883242844", ConvertVectorListToString(CollectablePoints));
        form.AddField("entry.883242844", ConvertListToString(death_reasons));


        UnityWebRequest WWW = UnityWebRequest.Post(URL, form);
        yield return WWW.SendWebRequest();
    }
    string ConvertVectorListToString(List<Vector2> vectors)
    {
        string result = string.Join("*", vectors.Select(v => $"({v.x}, {v.y})"));
        return result;
    }
    public void Lost()
    {

        Debug.Log("Did not reach end");
        Debug.Log("Collectable locations" + ConvertVectorListToString(CollectablePoints));
        lostCanvas.SetActive(true);
        timelimit = 0;
        Time.timeScale = 0f;
        Timetaken = time_running;
        Send();

    }
    public void LoseLife()
    {
        if (number_of_lives>=0)
            lives[number_of_lives].enabled = false;
        number_of_lives--;
        if (number_of_lives == -1)
            Lost();
    }
    private string ConvertListToString(List<string> list)
    {
        if (list == null || list.Count == 0)
        {
            return "()";
        }

        // Join the elements with a comma and space
        string joinedString = string.Join(", ", list);

        // Add parentheses to the beginning and end
        return "(" + joinedString + ")";
    }

    private void checkBoundary()
    {
        if (gameObject.transform.position.y > upBoundary || gameObject.transform.position.y < downBoundary)
        {
            gameObject.transform.position = this.respawnPoint;
            LoseLife();
        }
    }



}
