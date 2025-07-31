using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;

    private void Awake()
    {
        //need this so that this game object persits across all scenes
        //by default, when a new scene is loaded, all previous scenes are unloaded and game objects destroyed
        //but we want this game object to persist!
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        //reset game state (score, lives, start at level 1)
        this.score = 0;

        //LoadLevel(1);
    }

    private void LoadLevel(int levelNum)
    {
        //this.level = levelNum;
        //the parameter for LoadScene is either the scene index, as seen in File>BuildProfiles
        //or you can give the scene name as the parameter (this way is typically preferred since build indices can easily change)
        //SceneManager.LoadScene("Level" + levelNum); //follow this convention for all levels!

    }
}
