using UnityEngine;

public class Player : MonoBehaviour
{
    float moveSpeed = 3.0f;
    float jumpForce = 3.0f;

    Rigidbody2D rb;
    bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Left Arrow");
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
    }

    void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

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
}
