using UnityEngine;

public class NewCircle : MonoBehaviour
{
    [SerializeField] private bool hasBeenPassed;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lasso"))
        {
            hasBeenPassed = true;
        }
    }

    
}
