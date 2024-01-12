using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] GameObject player;
    [SerializeField] GameObject pausePanel;


    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (!isPaused)
            {
                pausePanel.SetActive(true);
                player.GetComponent<FirstPersonController>().cameraCanMove = false;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            } else
            {
                pausePanel.SetActive(false);
                player.GetComponent<FirstPersonController>().cameraCanMove = true;
                player.GetComponent<FirstPersonController>().lockCursor = true;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }
           
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }
}
