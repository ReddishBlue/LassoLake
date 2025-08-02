using UnityEngine;

public class PenManager : MonoBehaviour
{
    private GameManager gm;

    public GameObject[] animalPrefabs;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
        List<string> animals = gm.getAnimals();
        foreach(string animal in animals){
            spawnAnimal(animal);
        }


    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter");
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerController = other.gameObject.GetComponent<PlayerMovement>();
            playerController.registerPen(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        Debug.Log("exit");
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerController = other.gameObject.GetComponent<PlayerMovement>();
            playerController.deRegisterPen(this);
        }
    }


    public void spawnAnimal(string animalType){
        gm.addAnimal(animalType);
        foreach(GameObject animal in animalPrefabs){
            if(animal.name == animalType){
                Instantiate(animal, this.transform.position, Quaternion.identity);
            }
        }
    }
}
