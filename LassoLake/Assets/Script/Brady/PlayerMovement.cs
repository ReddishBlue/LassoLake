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
    public GameObject lasso;
    public GameObject lassoPattern;
    public bool inPen;
    private GameManager gm;

    // public string inventory;

    public bool inLassoMode;
    PenManager currentPen;

    

    Rigidbody2D rbody;

    


    public void enteredPen(){
        inPen = true;
    }

    public void exitedPen(){
        inPen = false;
    }

    public void registerPen(PenManager penManager){
        currentPen = penManager;
        enteredPen();
    }

    public void deRegisterPen(PenManager penManager){
        currentPen = null;
        exitedPen();
    }


    public void lassoCompleted(bool successful)
    {

        //Debug.Log("lasso playermove");
        if (selectedAnimal == null)
        {
            //Debug.Log("null animal");
            return;
        }
        AnimalFSM animalFSM = selectedAnimal.GetComponentInChildren<AnimalFSM>();
        animalID id = selectedAnimal.GetComponent<animalID>();
        gm.storeAnimal(id.getName());
        // inventory = id.getName();
        animalFSM.lassoCompleted(successful);
        Destroy(lasso);
        Destroy(lassoPattern);
        inLassoMode = false;

    }

    private void Awake()
    {
        inLassoMode = false;
        
        // inventory = "none";
        playerInputActions = new LassoLakeInput();
        rbody = GetComponent<Rigidbody2D>();
        charRenderer = GetComponent<MyCharacterRenderer>();
        gm = FindAnyObjectByType<GameManager>();
        // inventory = gm.getInventory();

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

    public void depositAnimal(){
        currentPen.spawnAnimal(gm.getInventory());
        gm.clearInventory();
    }

    private void onInteract()
    {

        if(inLassoMode){
            return;
        }

        if(inPen){
            if(gm.InventoryIsFull()){
                depositAnimal();
            }
            return;
        }
        if(gm.InventoryIsFull()){
            return;
        }
        GameObject closestAnimal = findClosestEnemyInRange();
        if (closestAnimal == null)
        {
            return;
        }
        inLassoMode = true;
        selectedAnimal = closestAnimal;
        AnimalFSM animalScript = closestAnimal.GetComponent<AnimalFSM>();
        animalScript.doLasso();
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0); 
        // cameraZDistance would be the z-position of your 2D objects in world space
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);
        worldCenter.z = 0;
        lassoPattern = Instantiate(lassoPatternPrefab, worldCenter, Quaternion.identity);
        Circle2 lassoPatternScript = lassoPattern.GetComponent<Circle2>();
        lassoPatternScript.setPlayerControllerReference(this);
        lasso = Instantiate(lassoPrefab, transform.position, Quaternion.identity);
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

    private void FixedUpdate() 
    {
        if(!inLassoMode){
            Movement();
        }
    }


    private Vector2 MovePlayer(Vector2 movement)
    {
        
        Vector2 currentPos = rbody.position;
        Vector2 newPos = currentPos + movement;

        
        if(charRenderer != null)
            charRenderer.SetMoveDirection(movement);
        rbody.MovePosition(newPos);

        return newPos;
    }

    private void Movement()
    {
        Vector2 input = playerInputActions.Player.Move.ReadValue<Vector2>();
        Vector2 movement = input * (movementSpeed * Time.deltaTime);

        MovePlayer(movement);
        

    }
}
