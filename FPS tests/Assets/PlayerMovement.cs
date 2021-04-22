using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController; // store a refrence of the charatercontroller

    public Transform mainCamera;
    public float jumpHeight;
    public float gravity;


    Vector3 playerVelocity;
    public float moveSpeed;
    public float airMovementMultiplyer;
    public float mouseSens;
    float trueMoveSpeed; // this is the move speed that will be used for caculations, this will change when the player is mid air, so they dont have perfect air controll
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        trueMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if(!characterController.isGrounded)
        {
            trueMoveSpeed = moveSpeed * airMovementMultiplyer; // add a multipleyer if there midair so aircontroll is not perfect
        }
        else
        {
            trueMoveSpeed = moveSpeed; // when the player is grounded again reset movespeed

        }
        NewMovement();

      
    }


    void NewMovement()
    {
        bool groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = Input.GetAxis("Vertical") * trueMoveSpeed * Time.deltaTime * transform.TransformDirection(Vector3.forward) + Input.GetAxis("Horizontal") * (trueMoveSpeed / 2) * Time.deltaTime * transform.TransformDirection(Vector3.right);
        characterController.Move(move * moveSpeed);

       

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSens, 0));
            mainCamera.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * mouseSens, 0, 0));
        
        
       



    }

}
