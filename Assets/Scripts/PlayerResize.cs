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

    private int mushrooms = 0;
    [SerializeField] private TMP_Text MushroomsText;
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
        playerTransform = player.GetComponent<Transform>();
        originalScale = playerTransform.localScale;
        ogscale = true;
        resizeTimer = Time.deltaTime;
        mushrooms = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ogscale && mushrooms>0)
            {
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
            if (resizeTimer > 10)
            {
                playerTransform.localScale = originalScale;
                ogscale = true;
                resizeTimer = 0;
            }
        }
    }
}
