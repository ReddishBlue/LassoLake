using UnityEngine;

public class animalID : MonoBehaviour
{

    public string ID;

    public string getName(){
        return ID;
    }

    public void setName(string newName){
        ID = newName;
    }
}
