using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController; // store a refrence of the charatercontroller

    public Transform mainCamera;
    public float jumpHeight;
    public float gravity;

    public float moveSpeed;
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
       
        NewMovement();
    }


    void NewMovement()
    {
        Vector3 finalMovement;

        finalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime * transform.TransformDirection(Vector3.forward) + Input.GetAxis("Horizontal") * (moveSpeed / 2) * Time.deltaTime * transform.TransformDirection(Vector3.right);

        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space)) // check if the player is grounded beforle letting them jump, so that you cant infinite jump
        {
            finalMovement.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            Debug.Log("Jumped");
        }

        finalMovement.y += gravity * Time.deltaTime;

        characterController.Move(finalMovement);
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
        mainCamera.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0));



    }

}
