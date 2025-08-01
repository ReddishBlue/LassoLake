using UnityEngine;
using UnityEngine.SceneManagement; //allows us to use SceneManager class

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public string level; //string for level name since theres just two main screens, and one tutorial

    //TODO: change to type animal
    public LittleGuyTest[] activeAnimals;
    
    //public Animal[] inventory;

    private void Awake()
    {
        //need this so that this game object persits across all scenes
        //by default, when a new scene is loaded, all previous scenes are unloaded and game objects destroyed
        //but we want this game object to persist!
        DontDestroyOnLoad(this.gameObject);

        //subscribe to a unity even every time scene is loaded so we can get references to this level's ball and paddle
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        //reset game state (score, lives, start at level 1)
        this.score = 0;
        //this.activeAnimals = 0;

        //TODO this should start on a Main Menu probably
        //LoadLevel("Tutorial");
    }

    public void LoadLevel(string levelName)
    {
        this.level = levelName;
        
        SceneManager.LoadScene(levelName); 

    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        //all animals as variants of a base animal. they share a script!
        activeAnimals = FindObjectsByType<LittleGuyTest>(FindObjectsSortMode.None);
    }
}
