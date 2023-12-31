using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GravityManipulation : MonoBehaviour
{
    public Image antiGravityTimerImage;

    GameObject player ;
    float gravityTimer;
    public Button antiGravityButton;
    private Transform playerTransform;
    private Vector3 originalScale;

    bool isAntiGravityActive;
    private float antiGravityDuration = 5.0f;
    [SerializeField] private TMP_Text AGText;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        originalScale = playerTransform.localScale;
        isAntiGravityActive = false;
        gravityTimer = 0;

        if (antiGravityButton!=null){
            antiGravityButton.interactable=true;
        }

        if (antiGravityTimerImage != null)
        {
            antiGravityTimerImage.fillAmount = 0;
        }

        if (AGText != null)
        {
            AGText.gameObject.SetActive(false);
        }
        // Rigidbody rb = player.GetComponent<Rigidbody>();
        // Vector3 direction = player.transform.position - transform.position;
        // rb.velocity  = new Vector2(direction.x, direction.y).normalized * force;

        // float rot = Mathf.Atan2(-direction.y, -direction.x)*Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0, 0, rot+90);
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = player.GetComponent<Transform>();
        if (Mathf.Abs(playerTransform.localScale.y - originalScale.y) > float.Epsilon ||
            Mathf.Abs(playerTransform.localScale.z - originalScale.z) > float.Epsilon)
        {
            antiGravityButton.interactable = false;
        }
        else
        {
            antiGravityButton.interactable = true;
        }

        if (isAntiGravityActive)
        {
            gravityTimer+= Time.deltaTime;

            float timeLeft = antiGravityDuration - gravityTimer;

            if (antiGravityTimerImage != null)
            {
                antiGravityTimerImage.fillAmount = timeLeft / antiGravityDuration;
            }
            if (AGText != null)
            {
                AGText.gameObject.SetActive(true);
            }

            if (gravityTimer > antiGravityDuration)
            {
                if (antiGravityTimerImage != null)
                {
                    // Reset radial timer fill amount when anti-gravity is not active
                    antiGravityTimerImage.fillAmount = 0;
                }

                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                rb.gravityScale=1;
                isAntiGravityActive = false;
                gravityTimer = 0;  
                antiGravityButton.interactable=true;
                return; 


        }

        }
        if (!isAntiGravityActive)
        {
            if (AGText != null)
            {
                AGText.gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.G) & !isAntiGravityActive)
        {
            Debug.Log("Antigravity active");
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.gravityScale = -1;
            Player.antigravity++;
            Player.current_mechs.Add("Antigravity");
            antiGravityButton.interactable = false;
            isAntiGravityActive = true;
            gravityTimer = 0;
        } else if(Input.GetKeyDown(KeyCode.G) & isAntiGravityActive)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            Debug.Log("Antigravity inactive");
            rb.gravityScale = 1;
            isAntiGravityActive = false;
            gravityTimer = 0;
            antiGravityTimerImage.fillAmount = 0;
        }


    }
    public void ButtonClicked()
    {
        if (antiGravityButton != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.gravityScale = -1;
            Player.antigravity++;
            Player.current_mechs.Add("Antigravity");
            antiGravityButton.interactable = false;
            isAntiGravityActive = true;
            gravityTimer = 0; // Reset the timer
        }
    }
}
