using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    private Dictionary<string, string> levelDict = new Dictionary<string, string>();

    // Start is called before the first frame update
    private void Start()
    {
        PauseMenu.SetActive(false);
        levelDict.Add("Tutorial_1", "Tutorial_2");
        levelDict.Add("Tutorial_2", "Tutorial_3");
        levelDict.Add("Tutorial_3", "MazeScene 3");

    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        //go back to main scene
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void GoNextLevel()
    {
        Player player = FindObjectOfType<Player>();

        Player.isPowerUpOn = false;
        SceneManager.LoadScene(levelDict[SceneManager.GetActiveScene().name]);
        Time.timeScale = 1f;
    }
}
