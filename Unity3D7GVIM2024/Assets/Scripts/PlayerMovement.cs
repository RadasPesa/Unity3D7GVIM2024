using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction movementAction;
    private Vector2 inputDirection;
    private Vector3 moveDirection;
    
    public float moveSpeed = 5f;
    
    //Rigidbody movement
    private Rigidbody rb;
    
    // Character controller
    private CharacterController characterController;
    
    private Transform cameraTransform;

    [SerializeField] private GameObject playerModel;
    private Animator playerAnim;

    [SerializeField] private float rotationSpeed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        movementAction = playerInput.actions["Movement"];
        
        //rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        
        playerAnim = playerModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = movementAction.ReadValue<Vector2>();
        moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
        moveDirection = cameraTransform.forward * moveDirection.z +
                        cameraTransform.right * moveDirection.x;
        moveDirection.y = 0f;

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            playerModel.transform.rotation = Quaternion.Lerp(
                playerModel.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        MovePlayerCC();
        
        playerAnim.SetFloat("Speed", moveDirection.sqrMagnitude);
    }

    private void MovePlayerRB()
    {
        Vector3 moveVector = transform.TransformDirection(moveDirection) * moveSpeed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
    }

    private void MovePlayerCC()
    {
        Vector3 moveVector = transform.TransformDirection(moveDirection);
        characterController.Move(moveVector * (moveSpeed * Time.deltaTime));
    }
}
