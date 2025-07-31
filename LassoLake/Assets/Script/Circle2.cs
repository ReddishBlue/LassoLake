//electric boogaloo
using UnityEngine;

public class Circle2 : MonoBehaviour
{
    [SerializeField] private int checkpointsPassed = 0;
    [SerializeField] private int checkpointThreshold = 6; //theres 7 total

    [SerializeField] private bool wasStartPassed = false;
    [SerializeField] private bool wasEndPassed = false;

    private void Update()
    {
        if (checkShapeDrawn())
        {
            //exit lasso mode
        }
    }

    private bool checkShapeDrawn()
    {
        if (wasStartPassed && wasEndPassed)
        {
            if (checkpointsPassed >= checkpointThreshold)
            {
                return true;
            }
        }
        return false;
    }

    public void incrementCheckpoint()
    {
        if (wasStartPassed)
        {
            checkpointsPassed++;
        }
    }
    public void startPassed()
    {
        wasStartPassed = true;
    }
    public void endPassed()
    {
        wasEndPassed = true;
    }
}
