using UnityEngine;

public class PensToLake : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D otherCol)
    {
        //Debug.Log("trigger endered");
        
        if (otherCol.gameObject.CompareTag("Player"))
        {
            gm.LoadLevel("Lake");
        }
    }
}
