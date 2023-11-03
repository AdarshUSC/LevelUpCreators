using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Player.playerMoveSpeed = 8f;
    }


    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit.");
    }
}
