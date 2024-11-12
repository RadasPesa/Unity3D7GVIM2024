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
    
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        movementAction = playerInput.actions["Movement"];
        
        //rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = movementAction.ReadValue<Vector2>();
        moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);

        MovePlayerCC();
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
