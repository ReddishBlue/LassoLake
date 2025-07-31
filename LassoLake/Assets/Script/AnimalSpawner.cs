using UnityEngine;
using System.Collections;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 6f;

    //TODO: change to public Animal once animal script is good to go
    [SerializeField] private GameObject[] animalPrefabs; //selects random animal prefab from this array

    [SerializeField] private bool canSpawn = true;

    public int numActiveAnimals = 0;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    public void captureAnimal()
    {
        numActiveAnimals--;
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        
        if (numActiveAnimals < 5)
        {
            canSpawn = true;
        }
    
        while (canSpawn)
        {
            if (numActiveAnimals >= 5)
            {
                canSpawn = false;
            }

            yield return wait;

            int rand = Random.Range(0, animalPrefabs.Length);
            GameObject animal = animalPrefabs[rand];

            Instantiate(animal, transform.position, Quaternion.identity); //do random position outside player visual window
            LittleGuyTest animScript = animal.GetComponent<LittleGuyTest>();
            animScript.mySpawner = this;

            numActiveAnimals++; //TODO detect when an animal is no longer active from THIS spawner


        }
    }
}
