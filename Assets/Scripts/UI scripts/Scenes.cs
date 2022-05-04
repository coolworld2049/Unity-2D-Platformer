using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void LoadScene(int numberScene)
    {
        SceneManager.LoadScene(numberScene); // загрузка сцены по номеру в Build Setting
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex <= 4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // загрузка сцены по номеру в Build Setting
        }
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit(); // выход из игры на рабочий стол
    }
}
