using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
    }
    public void StartGame()
    {
        gm.LoadLevel("Pens");
    }
}
