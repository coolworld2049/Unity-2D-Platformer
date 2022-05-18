using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI_scripts
{
    public class GameEndMenu : MonoBehaviour
    {
        public TMP_Text currentScore;
        public TMP_Text highScore;
        public GameObject winLoseMenuEndGame;
        public GameObject winLoseMenuNextLevel;
        public GameObject pauseMenu;
        public GameObject interfaceUI;
        public Health playerHealth;
        [FormerlySerializedAs("ch")] public CharacterMove player;

        private void Update()
        {
            if (playerHealth.HealthInitial <= 0f)
            {
                winLoseMenuEndGame.SetActive(true);
                Time.timeScale = 0f;

                pauseMenu.SetActive(false);
                interfaceUI.SetActive(false);

                currentScore.text = player.countCrystall.ToString();
                highScore.text = player.countCrystall.ToString();
            }
            
            else
            {
                Time.timeScale = 1f;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("collision.gameObject.tag == Player");
                if (SceneManager.GetActiveScene().buildIndex < 4)
                {
                    winLoseMenuNextLevel.SetActive(true);
                    Time.timeScale = 0f;

                    pauseMenu.SetActive(false);
                    interfaceUI.SetActive(false);

                    currentScore.text = player.countCrystall.ToString();
                    highScore.text = player.countCrystall.ToString();
                }

                else if (SceneManager.GetActiveScene().buildIndex == 4) // загрузка winLoseMenuGameEnd со счетом
                {
                    winLoseMenuEndGame.SetActive(true);
                    Time.timeScale = 0f;

                    pauseMenu.SetActive(false);
                    interfaceUI.SetActive(false);

                    currentScore.text = player.countCrystall.ToString();
                    highScore.text = player.countCrystall.ToString();
                }
            }
        }
    }
}
