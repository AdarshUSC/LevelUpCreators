using UnityEngine;

public class ReflectionPower : MonoBehaviour
{
    public GameObject mirror;
    public GameObject shadow;

    private GameObject myMirror;
    private GameObject myShadow;
    private Vector3 direction;
    private Vector3 curPos;
    private bool isAbletoPut;
    private bool isAbletoGetBack;
    

    // Start is called before the first frame update
    void Start()
    {
        direction = gameObject.transform.position.normalized;
        curPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position != curPos)
        {
            direction = (gameObject.transform.position - curPos).x > 0? Vector2.right : Vector2.left;
        }
        curPos = gameObject.transform.position;
        if (Input.GetKeyDown(KeyCode.Tab) && isAbletoPut)
        {
            if (myMirror)
            {
                //if (isAbletoGetBack)
                //{
                    Debug.Log("Get Mirror Back.");
                    if (isAbletoGetBack)
                        gameObject.transform.position = myShadow.transform.position;
                    Destroy(myMirror);
                    Destroy(myShadow);
            //}
        } else
            {
                Debug.Log("Mirror Put!");
                myMirror = Instantiate(mirror, transform.position + direction/5, Quaternion.identity);
                myShadow = Instantiate(shadow, transform.position + direction/5, Quaternion.identity);
            }
         
        }

        // if any objects block on the way between player and mirror
        if (myMirror)
        {
            RaycastHit2D hitleft = Physics2D.Raycast(myMirror.transform.position, Vector2.left);
            RaycastHit2D hitright = Physics2D.Raycast(myMirror.transform.position, Vector2.right);
            Debug.Log(hitright.transform.name);
            if ((hitleft.collider != null && hitleft.collider.gameObject.transform.tag == "Player") || (hitright.collider != null && hitright.collider.gameObject.transform.tag == "Player"))
            {
                myShadow.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                isAbletoGetBack = true;
            }
            else
            {
                myShadow.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                isAbletoGetBack = false;
            }
        }

        if (myShadow)
        {
            // need to add when collide with the wall.
            myShadow.transform.position = new Vector2(myMirror.transform.position.x + (myMirror.transform.position.x - gameObject.transform.position.x), gameObject.transform.position.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isAbletoPut = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isAbletoPut = false;
        // enable this after add tag to bricks
        //Destroy(myMirror);
        //Destroy(myShadow);
    }
}
