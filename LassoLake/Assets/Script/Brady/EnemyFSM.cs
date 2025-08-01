using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum animalState {idle, moving, lasso, deciding, captured}

    public animalState currentState = animalState.deciding;
    private Vector2 moveDir;
    public int moveSpeed = 5;
    public int idleTime = 3;
    public int moveInterval = 2;
    private bool changeDirection;
    private bool currentlyIdle = false;
    private bool currentlyMoving = false;
    private Animator animator;
    Rigidbody2D rbody;

    private void Awake()
    {
        //cache the animator component
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(currentState);
        if (currentState == animalState.idle) {
            idle();
        } else if (currentState == animalState.moving) {
            moving();
        }
        else if (currentState == animalState.lasso)
        {
            lasso();
        }
        else if (currentState == animalState.captured)
        {
            captured();
        }
        else if (currentState == animalState.deciding)
        {
            deciding();
        }
    }

    void idle()
    {
        if (!currentlyIdle)
        {
            StartCoroutine(idleTimer(idleTime));
        }
      //if co-routine is not already running, start coroutine that counts a certain amount of time
      
    }
    
    
    private IEnumerator idleTimer(float time)
    {
        currentlyIdle = true;
        animator.Play("idle");
        yield return new WaitForSeconds(time);
        currentState = animalState.deciding;
        currentlyIdle = false;
        
    }
    private IEnumerator moveTimer(float time)
    {
        currentlyMoving = true;
        yield return new WaitForSeconds(time);
        currentState = animalState.idle;
        currentlyMoving = false;
        
    }

    void moving()
    {
        if (!currentlyMoving)
        {
            StartCoroutine(moveTimer(moveInterval));   
        }
        else
        {
            animator.Play("moving");
            Vector2 movement = moveDir * (moveSpeed * Time.deltaTime);
            Vector2 currentPos = rbody.position;
            Vector2 newPos = currentPos + movement;
            
            rbody.MovePosition(newPos);
            // MovePlayer(movement);
        }
        
        if (changeDirection)
        {
            currentState = animalState.deciding;
        }
        
        
    }

    void lasso()
    {
        //make sounds?
    }

    void deciding()
    {
        //generate random direction
        int xDir = Random.Range(-1, 1);
        int yDir = Random.Range(-1, 1);
        if (xDir == 0 && yDir == 0)
        {
            if (Random.Range(0, 1) == 0)
            {
                xDir = 1;
            }
            else{
                yDir = 1;
            }
        }
        moveDir = new Vector2(xDir, yDir);
        currentState = animalState.moving;
        Debug.Log(moveDir);

    }

    void captured()
    {
        
    }
}
