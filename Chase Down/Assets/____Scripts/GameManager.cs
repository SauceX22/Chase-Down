using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Transform StartPoint;
    public GameObject GameOverPanel;

    bool GameHasEnded = false;

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        if (GameHasEnded == false)
        {
            Debug.Log("Game Over!");
            GameOverPanel.SetActive(true);
            player.GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            player.GetComponent<weapon>().enabled = false;
        }

    }

    public void ResetPlayerPos()
    {
        player.gameObject.transform.position = StartPoint.transform.position;
        Debug.Log("Player Teleported");
    }

    public void ResetTimeScaleAndWeapon()
    {
        Time.timeScale = 1f;
        player.GetComponent<weapon>().enabled = true;
    }
}
