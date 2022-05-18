using UnityEngine;

public class GamePause : MonoBehaviour
{
    public GameObject pauseMenu;
    private static bool gameIsPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // игра останавливается если была нажата клавиша Escape
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // возобновлеие процесса игры
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // остановка процесса игры
        gameIsPaused = true;
    }

}
