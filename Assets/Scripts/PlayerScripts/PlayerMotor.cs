using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private bool isGrounded;
    public float gravity = -9.8f;
    private Vector3 playerVelocity;
    public float walkSpeed = 5f;
    public float jumpHeight = 3f;
    public float runSpeed = 10f;
    private bool isRunning;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if(Input.GetKey(KeyCode.LeftShift)){
            isRunning = true;
        }else{
            isRunning = false;
        }
    }
    //receive the inputs for InputManager.cs and apply it to the character controller
    public void ProcessMove(Vector2 input){
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        float currentSpeed = isRunning?runSpeed:walkSpeed; // check the speed wether running or not 
        controller.Move(transform.TransformDirection(moveDirection) * currentSpeed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0){
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        // Debug.Log(playerVelocity.y);
    }
    public void Jump(){
        if(isGrounded){ // wether on grounded or not
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
