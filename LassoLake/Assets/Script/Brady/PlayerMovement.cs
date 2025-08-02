using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 100f;
    
    private LassoLakeInput playerInputActions;
    // public Animator animator;
    
    public MyCharacterRenderer charRenderer;
    public float detectionRadius = 3f; // Set in Inspector
    public LayerMask enemyLayer;       // Assign only enemy layers in Inspector
    GameObject selectedAnimal;
    public GameObject lassoPrefab;
    public GameObject lassoPatternPrefab;

    public string inventory;

    

    Rigidbody2D rbody;


    public void lassoCompleted(bool successful)
    {

        //Debug.Log("lasso playermove");
        if (selectedAnimal == null)
        {
            //Debug.Log("null animal");
            return;
        }
        AnimalFSM animalFSM = selectedAnimal.GetComponentInChildren<AnimalFSM>();
        if(successful){
            
        }
        animalFSM.lassoCompleted(successful);
    }

    private void Awake()
    {
        inventory = "none";
        playerInputActions = new LassoLakeInput();
        rbody = GetComponent<Rigidbody2D>();
        charRenderer = GetComponent<MyCharacterRenderer>();

    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        Debug.Log("added");
        playerInputActions.Player.Interact.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Player.Interact.performed -= OnInteractPerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        // Debug.Log("started");
        onInteract();
    }

    private void onInteract()
    {
        GameObject closestAnimal = findClosestEnemyInRange();
        if (closestAnimal == null)
        {
            return;
        }
        selectedAnimal = closestAnimal;
        AnimalFSM animalScript = closestAnimal.GetComponent<AnimalFSM>();
        animalScript.doLasso();
        GameObject outline = Instantiate(lassoPatternPrefab, transform.position, Quaternion.identity);
        Circle2 lassoTemplateScript = outline.GetComponent<Circle2>();
        lassoTemplateScript.setPlayerControllerReference(this);

        Instantiate(lassoPrefab, transform.position, Quaternion.identity);
    }

    private GameObject findClosestEnemyInRange(){
        // Debug.Log("started 2");
        Collider2D[] animalsInRange = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);

        if (animalsInRange.Length == 0)
        {
            Debug.Log("No animals in range.");
            return null;
        }

        Collider2D closestAnimal = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector2 currentPosition = transform.position;

        foreach (Collider2D animal in animalsInRange)
        {
            Vector2 directionToAnimals = (Vector2)animal.transform.position - currentPosition;
            float distanceSqr = directionToAnimals.sqrMagnitude;  

            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestAnimal = animal;
            }
        }

        if (closestAnimal != null)
        {
            Debug.Log("Closest animal is: " + closestAnimal.name);
        }

        return closestAnimal.gameObject;


    }

    private void OnDrawGizmosSelected()
{
    // Default to red (no enemies found)
    Color gizmoColor = Color.red;

    // Only try to detect enemies if in play mode and this GameObject is active
    if (Application.isPlaying)
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);

        if (enemiesInRange.Length > 0)
        {
            gizmoColor = Color.green; // Enemy found!
        }
    }

    Gizmos.color = gizmoColor;
    Gizmos.DrawWireSphere(transform.position, detectionRadius);
}

    private void Update() 
    {
        Movement();
        
    }

    public void OnExit()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //
    // private Vector3 VectorToCursor()
    // {
        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate direction vector from prefab to mouse
        // Vector3 direction = mousePosition - transform.position;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // return direction;
    // }

    private Vector2 MovePlayer(Vector2 movement)
    {
        
        Vector2 currentPos = rbody.position;
        Vector2 newPos = currentPos + movement;
        // if (animationController != null)
        // {
        //     if (movement.magnitude > 0.1f)
        //     {
        //         if (!animationController.walking)
        //         {
        //             animationController.StartedWalking();
        //         }
        //     }
        //     else
        //     {
        //         if (animationController.walking)
        //         {
        //             animationController.StoppedWalking();
        //         }
        //     }
        // }
        
        if(charRenderer != null)
            // charRenderer.SetLookDirection(VectorToCursor());
            charRenderer.SetMoveDirection(movement);
        rbody.MovePosition(newPos);

        return newPos;
    }

    private void Movement()
    {
        Vector2 input = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        // Debug.Log("speed = " + speed);
        Vector2 movement = input * (movementSpeed * Time.deltaTime);

        MovePlayer(movement);
        

    }
}
