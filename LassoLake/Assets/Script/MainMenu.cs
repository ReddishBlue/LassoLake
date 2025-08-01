using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameManager gm;
    private PauseMenu pm;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
        pm = FindAnyObjectByType<PauseMenu>();
    }
    public void StartGame()
    {
        gm.LoadLevel("Tutorial");
    }

    public void ExitGame()
    {
        //Debug.Log("Quit"); //to see if it actually works
        Application.Quit();
    }

    public void toMainMenu()
    {
        Time.timeScale = 1f;
        gm.LoadLevel("MainMenu");
    }

    public void ResumeGame()
    {
        pm.ResumeGame();
    }
}
