using UnityEngine;

public class TutorialCow : MonoBehaviour
{
    //private bool isLassoed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void doLasso()
    {
        //empty on purpose
    }

    public void lassoCompleted(bool success)
    {

        //Debug.Log("lasso animalfsm");
        if (success)
        {
            captured();
        }

    }

    void captured()
    {
        Debug.Log("captured");
        Destroy(this.gameObject);
    }
}
