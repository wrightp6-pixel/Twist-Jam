using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Starting game");
        // Load the 1st level
        SceneManager.LoadSceneAsync("Level1");
    }

    public void LeaveGame()
    {
        Debug.Log("Quit");
        // Close the application
        Application.Quit();
    }

    public void ToTitle()
    {
        SceneManager.LoadSceneAsync("TitleScreen");
    }

    public void ToCredits()
    {
        SceneManager.LoadSceneAsync("Credits");
    }

    public void ToEnd()
    {
        SceneManager.LoadSceneAsync("Ending");
    }
}
