using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFSM : MonoBehaviour
{
    public enum animalState {idle, moving, lasso, deciding, captured}

    public animalState currentState = animalState.deciding;
    private Vector2 moveDir;
    public int moveSpeed = 5;
    public float idleTime = 3;
    public float moveInterval = 2;
    private bool changeDirection;
    private bool currentlyIdle = false;
    private bool currentlyMoving = false;
    private Animator animator;
    Rigidbody2D rbody;
    public string ID;
    private Vector2 lastDirection;

    private bool isLassoed;

    private void Awake()
    {
        //cache the animator component
        animator = GetComponentInChildren<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        moveDir = new Vector2(-1, 0);
    }

    public string getName(){
        return ID;
    }

    public void setName(string newName){
        ID = newName;
    }

    void Update()
    {
        // Debug.Log(currentState);
        if (currentState == animalState.lasso) {
            lasso();

        } else if (currentState == animalState.moving) {
            moving();
        }
        else if (currentState == animalState.idle)
        {
            idle();
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
        if(isLassoed){
            return;
        }
        if (!currentlyIdle)
        {
            StartCoroutine(idleTimer(idleTime));
        }
      //if co-routine is not already running, start coroutine that counts a certain amount of time
      
    }
    
    
    private IEnumerator idleTimer(float time)
    {
        currentlyIdle = true;
        if(lastDirection.x > 0){
            animator.Play("idle right");
        }
        else{
            animator.Play("idle left");
        }
        // animator.Play("idle");
        yield return new WaitForSeconds(time);
        if(!isLassoed)
            currentState = animalState.deciding;
        currentlyIdle = false;
        
    }
    private IEnumerator moveTimer(float time)
    {
        currentlyMoving = true;
        yield return new WaitForSeconds(time);
        if(!isLassoed)
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
            if(isLassoed){
                return;
            }
            if(moveDir.x > 0){
                animator.Play("moving right");
            }
            else{
                animator.Play("moving left");
            }
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
        Debug.Log("lasso");
        if(lastDirection.x > 0){
            animator.Play("idle right");
        }
        else{
            animator.Play("idle left");
        }
        isLassoed = true;
        currentlyMoving = false;
        //make sounds?
    }

    public void doLasso(){
        currentState = animalState.lasso;
        lasso();
    }

    public void lassoCompleted(bool success){
        if(success){
            currentState = animalState.captured;
        }
        else{
            currentState = animalState.deciding;
        }
    }

    void deciding()
    {
        if(isLassoed){
            return;
        }
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
        lastDirection = moveDir;
        moveDir.x = xDir;
        moveDir.y = yDir;
        Debug.Log(moveDir);
        currentState = animalState.moving;
        // Debug.Log(moveDir);

    }

    void captured()
    {
        Debug.Log("captured");
    }
}
