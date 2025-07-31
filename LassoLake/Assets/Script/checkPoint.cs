using UnityEngine;

public class NewCircle : MonoBehaviour
{

    SpriteRenderer sr;
    Color color = Color.black;
    [SerializeField] private bool hasBeenPassed;

    void Start()
    {
        sr = GetComponentInParent<SpriteRenderer>();
        sr.color = color;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lasso"))
        {
            hasBeenPassed = true;
        }
    }

    
}
