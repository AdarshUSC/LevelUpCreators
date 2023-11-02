using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResize : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 originalScale;
    private bool ogscale;
    float resizeTimer;
    GameObject player;
    private MiniPathDetector mpd;
    private Player play;
    public GameObject mpdObj;
    public GameObject playObj;

    private int mushrooms = 0;
    [SerializeField] private TMP_Text MushroomsText;
    [SerializeField] private TMP_Text resizeTimerText; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mushroom")
        {
            Destroy(collision.gameObject);
            mushrooms++;
            MushroomsText.text = mushrooms.ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mpd = mpdObj.GetComponent<MiniPathDetector>();
        play = playObj.GetComponent<Player>();
        playerTransform = player.GetComponent<Transform>();
        originalScale = playerTransform.localScale;
        ogscale = true;
        resizeTimer = Time.deltaTime;
        mushrooms = 0;

        if (resizeTimerText != null)
        {
            resizeTimerText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ogscale && mushrooms > 0)
            {
                Player.resize++;
                Player.current_mechs.Add("Resize");
                Vector3 newScale = new Vector3(3.0f, 3.0f, 3.0f);
                playerTransform.localScale = newScale;
                ogscale = false;
                mushrooms--;
                MushroomsText.text = mushrooms.ToString();
            }
        }
        if (!ogscale)
        {
            resizeTimer += Time.deltaTime;

            float timeLeft = 15f - resizeTimer;

            if (resizeTimerText != null)
            {
                if (timeLeft > 0)
                {
                    resizeTimerText.text = $"Time for resizing: {timeLeft.ToString("F2")}";
                }
                else
                {
                    resizeTimerText.text = ""; // Hide the text when the countdown is over
                }
            }

            if (resizeTimer > 15 && mpd.playerInside == false)
            {
                playerTransform.localScale = originalScale;
                ogscale = true;
                resizeTimer = 0;
            }
            else if (resizeTimer > 15 && mpd.playerInside == true)
            {
		Debug.Log("Calling lose life");
                playerTransform.position = p.respawnPoint;
                play.LoseLife();
                resizeTimer = 0;
                playerTransform.localScale = originalScale;
            }
        }

    }
}
