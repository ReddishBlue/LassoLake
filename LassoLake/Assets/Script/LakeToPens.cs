using UnityEngine;

public class LakeToPens : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gm.LoadLevel("Pens");
        }
    }
}
