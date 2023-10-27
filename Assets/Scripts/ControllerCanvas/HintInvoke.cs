using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintInvoke : MonoBehaviour
{
    [SerializeField] GameObject HintPannel;
    [SerializeField] GameObject PauseButton;

    private bool hintDisabled = true;
    // Start is called before the first frame update
    void Start()
    {
        HintPannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hintDisabled && Input.GetKey(KeyCode.Space))
        {
            HintPannel.SetActive(false);
            Time.timeScale = 1f;
            hintDisabled = true;
            PauseButton.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="Player")
        {
            HintPannel.SetActive(true);
            Time.timeScale = 0f;
            hintDisabled = false;
            PauseButton.SetActive(false);
        }
    }
}
