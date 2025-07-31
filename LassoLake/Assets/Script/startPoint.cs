using UnityEngine;

public class startPoint : MonoBehaviour
{
    [SerializeField] private bool wasPassedOnce;
    [SerializeField] private bool wasPassedTwice;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lasso"))
        {
            if (!wasPassedOnce) { wasPassedOnce = true; }
            else if (wasPassedOnce) { wasPassedTwice = true; }
        }
    }
}