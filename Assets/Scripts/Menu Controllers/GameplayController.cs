using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;

    void Awake()
    {
        pausePanel.SetActive(false);
    }


    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("MainMenu");
    }
}
