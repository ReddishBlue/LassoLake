using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private InputAction escapeKey;
    private bool isPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
        escapeKey = InputSystem.actions.FindAction("Escape");
    }

    // Update is called once per frame
    void Update()
    {
        if (escapeKey.ReadValue<float>() == 1f)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; //stop time
        isPaused = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; //resume time
        isPaused = false;
    }
}
