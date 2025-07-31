using UnityEngine;

public class NewCircle : MonoBehaviour
{
    [SerializeField] private bool hasBeenPassed;
    //private Circle2 mainShape = gameObject.AddComponent<Circle2>();

    private void Start()
    {
        //mainShape = gameObject.AddComponent<Circle2>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lasso"))
        {
            hasBeenPassed = true;
            gameObject.GetComponentInParent<Circle2>().incrementCheckpoint();
        }
    }

    
}
