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
                Debug.Log("Put Mirror Back.");
                gameObject.transform.position = myShadow.transform.position;
                Destroy(myMirror);
                Destroy(myShadow);
            } else
            {
                Debug.Log("Mirror Put!");
                myMirror = Instantiate(mirror, transform.position + direction/5, Quaternion.identity);
                myShadow = Instantiate(shadow, transform.position + direction/5, Quaternion.identity);
            }
         
        }
        Debug.Log(direction);
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
        // enable this after adding tag to bricks
        //Destroy(myMirror);
        //Destroy(myShadow);
    }
}
