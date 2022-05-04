using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndMenu : MonoBehaviour
{
    public TMP_Text currentScore;
    public TMP_Text highScore;

    public GameObject winLoseMenuEndGame;
    public GameObject winLoseMenuNextLevel;
    public GameObject pauseMenu;
    public GameObject interfaceUI;
    public Health playerHealth;
    public CharacterMove ch;

    private void Update()
    {    
        if (playerHealth.HealthInitial <= 0f)
        {
            winLoseMenuEndGame.SetActive(true);
            Time.timeScale = 0f;

            pauseMenu.SetActive(false);
            interfaceUI.SetActive(false);

            currentScore.text = ch.countCrystall.ToString();
            highScore.text = ch.countCrystall.ToString();
        }

        else if (playerHealth.HealthInitial >= 0f)
        {
            Time.timeScale = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex < 4)
            {
                winLoseMenuNextLevel.SetActive(true);
                Time.timeScale = 0f;

                pauseMenu.SetActive(false);
                interfaceUI.SetActive(false);

                currentScore.text = ch.countCrystall.ToString();
                highScore.text = ch.countCrystall.ToString();
            }

            else if (SceneManager.GetActiveScene().buildIndex == 4) // загрузка winLoseMenuGameEnd со счетом
            {
                winLoseMenuEndGame.SetActive(true);
                Time.timeScale = 0f;

                pauseMenu.SetActive(false);
                interfaceUI.SetActive(false);

                currentScore.text = ch.countCrystall.ToString();
                highScore.text = ch.countCrystall.ToString();
            }
        }
    }
}
