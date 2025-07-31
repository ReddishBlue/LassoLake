using UnityEngine;

public class startPoint : MonoBehaviour
{
    [SerializeField] private bool wasPassedOnce;
    [SerializeField] private bool wasPassedTwice;
    Circle2 mainShape;


    private void Start()
    {
        mainShape = gameObject.AddComponent<Circle2>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lasso"))
        {
            if (!wasPassedOnce)
            {
                wasPassedOnce = true;
                mainShape.startPassed();
            }
            else if (wasPassedOnce)
            {
                wasPassedTwice = true;
                mainShape.endPassed();
            }
        }
    }
}