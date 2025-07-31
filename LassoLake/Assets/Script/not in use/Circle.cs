using UnityEngine;
using System.Collections.Generic;

public class Circle : MonoBehaviour
{
    private List<Vector3> posList = new List<Vector3>();
    private LineRenderer line;
    [SerializeField] private float radius = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        for (int i = 0; i <= 360; i++)
        {
            float angleInRad = i * Mathf.PI / 180;
            posList.Add(new Vector3(radius*Mathf.Cos(angleInRad), radius*Mathf.Sin(angleInRad), 0));
        }

        line.positionCount = posList.Count;
        line.SetPositions(posList.ToArray());
    }

    public Vector3[] GetPosArray()
    {
        return posList.ToArray();
    }
}
