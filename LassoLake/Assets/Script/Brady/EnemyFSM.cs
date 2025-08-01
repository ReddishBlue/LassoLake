using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState {Scanning, AttackPlayer}

    public EnemyState currentState = EnemyState.Scanning;

    public int rotationSpeed = 20;
    private int rotationDirection = 1;

    // public EnemyAISight sightSensor;
    // public EnemyCombat enemyCombat;

    public float angleMin = 90;
    public float angleMax = 270;

    void Update()
    {
        if (currentState == EnemyState.Scanning) {
            Scanning();
        } else if (currentState == EnemyState.AttackPlayer) {
            AttackPlayer();
        }
    }

    void Scanning()
    {
        // if (sightSensor.detectedObject != null) {
        //     currentState = EnemyState.AttackPlayer;
        //     return;
        // }
        //
        // if(transform.rotation.eulerAngles.z >= angleMax)
        // {
        //     rotationDirection = -1;
        //     
        // }
        // if(transform.rotation.eulerAngles.z <= angleMin)
        // {
        //     rotationDirection = 1;
        // }
        //
        // transform.Rotate(0,0,rotationDirection* rotationSpeed*Time.deltaTime);

    }

    void AttackPlayer()
    {
        // if (sightSensor.detectedObject == null) {
        //     currentState = EnemyState.Scanning;
        //     return;
        // }
        //
        // Vector3 directionToController = 
        //     Vector3.Normalize(sightSensor.detectedObject.bounds.center - transform.position);
        //
        // float angleToCollider = Mathf.Atan2(directionToController.y, directionToController.x) * Mathf.Rad2Deg - 90; //offset by 90 to align with y
        //
        // transform.rotation = Quaternion.Euler(0, 0, angleToCollider);
        //
        // if (!enemyCombat.isOnCooldown) {
        //     StartCoroutine(enemyCombat.Fire());
        // }
        //
    }
}
