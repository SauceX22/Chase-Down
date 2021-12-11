using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject player;
    public GameObject settingsMenuUI;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                if (settingsMenuUI.activeInHierarchy)
                {
                    settingsMenuUI.SetActive(false);
                }
                else
                {
                    Resume();
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = false;

                }
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponent<weapon>().enabled = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<weapon>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void settingsMenu()
    {
        Debug.Log("settings, Loading...");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
    }
}
