using System;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class GamePause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject joystick;

    private static bool gameIsPaused = false;
    private Collider pauseButtonCollider;
    private Vector3 buttonPosition;

    private void Awake()
    {
        pauseButtonCollider = pauseButton.GetComponent<Collider>();
    }

    private void Update()
    {
#if UNITY_STANDALONE_WIN
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
#endif
#if UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider == pauseButtonCollider)
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
            }
        }
    }
#endif

    public void Resume()
    {
        pauseMenu.SetActive(false);
        joystick.SetActive(true);
        Time.timeScale = 1f; // возобновлеие процесса игры
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        joystick.SetActive(false);
        Time.timeScale = 0f; // остановка процесса игры
        gameIsPaused = true;
    }

}
