using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    // Start is called before the first frame update
    private void Start()
    {
        PauseMenu.SetActive(false);
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameObject.SetActive(true);
    }
}
