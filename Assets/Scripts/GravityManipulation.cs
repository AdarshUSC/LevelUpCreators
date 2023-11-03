using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GravityManipulation : MonoBehaviour
{
    GameObject player ;
    float gravityTimer;
    public Button antiGravityButton;


    [SerializeField] private TMP_Text AGTimerText;
    bool isAntiGravityActive;
    private float antiGravityDuration = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        isAntiGravityActive = false;
        gravityTimer = 0;

        if (antiGravityButton!=null){
            antiGravityButton.interactable=true;
        }

        if (AGTimerText != null)
        {
            AGTimerText.text = ""; // Initialize the text
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
        if(isAntiGravityActive)
        {
            gravityTimer+= Time.deltaTime;

            float timeLeft = antiGravityDuration - gravityTimer;


            if (AGTimerText != null)
            {
                if (timeLeft > 0)
                {
                    AGTimerText.text = $"Time for anti-gravity: {timeLeft.ToString("F2")}";
                }
                else
                {
                    AGTimerText.text = ""; // Hide the text when the countdown is over
                }
            }

            if (gravityTimer > antiGravityDuration)
            {
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                rb.gravityScale=1;
                isAntiGravityActive = false;
                gravityTimer = 0;  
                antiGravityButton.interactable=true;
                return; 
        }
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
