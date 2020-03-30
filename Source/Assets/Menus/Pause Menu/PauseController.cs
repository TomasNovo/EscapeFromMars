using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool paused = false;

    public GameObject pauseMenu;

    void Start()
    {
        paused = false;
        pauseMenu.SetActive(false);
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.instance.gameOver)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(paused)
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

    public void Menu()
    {
        pauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
        GameController.instance.DestroyScene();
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
