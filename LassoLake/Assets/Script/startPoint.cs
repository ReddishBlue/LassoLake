using UnityEngine;

public class startPoint : MonoBehaviour
{
    [SerializeField] private bool wasPassedOnce;
    [SerializeField] private bool wasPassedTwice;
    //private Circle2 mainShape;


    private void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lasso"))
        {
            if (!wasPassedOnce)
            {
                wasPassedOnce = true;
                gameObject.GetComponentInParent<Circle2>().startPassed();
            }
            else if (wasPassedOnce)
            {
                wasPassedTwice = true;
                gameObject.GetComponentInParent<Circle2>().endPassed();
            }
        }
    }
}