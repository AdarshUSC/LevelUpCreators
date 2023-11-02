using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintInvoke : MonoBehaviour
{
    [SerializeField] GameObject HintPannel;
    [SerializeField] GameObject PauseButton;

    public int timeToHit = 3;

    private bool hintDisabled = true;
    // Start is called before the first frame update
    void Start()
    {
        HintPannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hintDisabled && Input.GetKeyDown(KeyCode.Space))
        {
            HintPannel.SetActive(false);
            Time.timeScale = 1f;
            hintDisabled = true;
            PauseButton.SetActive(true);

            if (timeToHit <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="Player")
        {
            Debug.Log("Hint Hit!");
            HintPannel.SetActive(true);
            Time.timeScale = 0f;
            hintDisabled = false;
            PauseButton.SetActive(false);
            timeToHit -= 1;
        }
    }
}
