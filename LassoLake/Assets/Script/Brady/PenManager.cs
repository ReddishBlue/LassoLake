using UnityEngine;

public class PenManager : MonoBehaviour
{
    private GameManager gm;

    public GameObject[] animalPrefabs;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerController = other.gameObject.GetComponent<PlayerMovement>();
            playerController.registerPen(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerController = other.gameObject.GetComponent<PlayerMovement>();
            playerController.deRegisterPen(this);
        }
    }


    public void spawnAnimal(string animalType){
        foreach(GameObject animal in animalPrefabs){
            if(animal.name == animalType){
                Instantiate(animal, this.transform.position, Quaternion.identity);
            }
        }
    }
}
