using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class LassoTrace : MonoBehaviour
{
    [SerializeField] private Vector2 mousePos = Vector2.zero;
    public InputAction leftClick;

    [SerializeField] private bool isClicked = false;

    private List<Vector3> pointsList = new List<Vector3>();

    private LineRenderer line;
    

    private void Start()
    {
        mousePos = Vector2.zero;
        leftClick = InputSystem.actions.FindAction("Attack"); //"Attack" is mapped to left click by default
        line = gameObject.GetComponent<LineRenderer>();
    }

    private void onEnable()
    {
        leftClick.Enable();
    }
    private void onDisable()
    {
        leftClick.Disable();
    }

    private void Update()
    {
        if (Mouse.current != null)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = mousePos;
        }
        if (leftClick.ReadValue<float>() == 1f) { isClicked = true; }
        else { isClicked = false; }

        addToListOfPoints(mousePos);

        line.positionCount = pointsList.Count;
        line.SetPositions(pointsList.ToArray());

        toggleLasso();
    }

    private void toggleLasso()
    {
        if (isClicked)
        {
            gameObject.tag = "Lasso";
        }
        else
        {
            gameObject.tag = "Untagged";
        }
    }

    private void addToListOfPoints(Vector3 currPos)
    {
        if (isClicked)
        {
            pointsList.Add(new Vector3(mousePos.x, mousePos.y, 0));
        }
        else
        {
            pointsList.Clear();
        }
    }
}
