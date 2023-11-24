using UnityEngine;

public class ReflectionPower : MonoBehaviour
{
    public GameObject mirror;
    public GameObject shadow;
    public GameObject shadowCamera;
    public float cameraZoomSize;

    private GameObject myMirror;
    private GameObject myShadow;
    private GameObject myShadowCamera;
    private Vector3 direction;
    private bool isAbletoPut;
    private bool isAbletoGetBack;
    

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            direction = Vector2.right;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            direction = Vector2.left;

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
                Destroy(myShadowCamera);
                //}
            } else
            {
                Debug.Log("Mirror Put!");
                myMirror = Instantiate(mirror, transform.position + direction/5, Quaternion.identity);
                myShadow = Instantiate(shadow, transform.position + direction/5, Quaternion.identity);
                myShadowCamera = Instantiate(shadowCamera);

                Player.reflection++;
                Debug.Log("Adding reflection" + Player.current_mechs.Count);
                Player.current_mechs.Add("Reflection");
            }
         
        }

        if (myMirror && Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(myMirror);
            Destroy(myShadow);
            Destroy(myShadowCamera);
        }

        // if any objects block on the way between player and mirror
        if (myMirror)
        {
            RaycastHit2D hitleft = Physics2D.Raycast(myMirror.transform.position, Vector2.left);
            RaycastHit2D hitright = Physics2D.Raycast(myMirror.transform.position, Vector2.right);
            if ((hitleft.collider != null && hitleft.collider.gameObject.transform.tag == "Player") || (hitright.collider != null && hitright.collider.gameObject.transform.tag == "Player"))
            {
                myShadow.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.7f);
                isAbletoGetBack = true;
            }
            else
            {
                myShadow.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                isAbletoGetBack = false;
            }

            if (isAbletoGetBack && !myShadowCamera)
            {
                myShadowCamera = Instantiate(shadowCamera);
            } else if (!isAbletoGetBack && myShadowCamera)
            {
                Destroy(myShadowCamera);
            }
        }

        //if (myShadowCamera && !isAbletoGetBack)
        //{
        //    Destroy(myShadowCamera);
        //}
        //if (!myShadowCamera && isAbletoGetBack)
        //{
        //    myShadowCamera = Instantiate(shadowCamera);
        //}

        if (myShadow)
        {
            // need to add when collide with the wall.
            myShadow.transform.position = new Vector2(myMirror.transform.position.x + (myMirror.transform.position.x - gameObject.transform.position.x), gameObject.transform.position.y);
            if (myShadowCamera)
            {
                myShadowCamera.transform.position = new Vector3(myShadow.transform.position.x, myShadow.transform.position.y, -3);
                myShadowCamera.GetComponent<Camera>().orthographicSize = cameraZoomSize;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isAbletoPut = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isAbletoPut = false;
        //if (collision.transform.tag=="Ground")
        //{
        //    Destroy(myMirror);
        //    Destroy(myShadow);
        //}
    }

    public void clearAll()
    {
        if (myMirror)
        Destroy(myMirror);
        if (myShadow)
        Destroy(myShadow);
        if (myShadowCamera)
        Destroy(myShadowCamera);
    }
}
