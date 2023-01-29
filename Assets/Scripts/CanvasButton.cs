using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasButton : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
        GameController.noGame = true;
    }

    public static bool isPaused = false;

    public void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            PlaneController.engineSound.enabled = false;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
