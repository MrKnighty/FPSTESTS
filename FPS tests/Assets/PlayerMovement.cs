using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController; // store a refrence of the charatercontroller

    public Transform mainCamera;

    public float moveSpeed;
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        Vector3 fowardMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime * transform.TransformDirection(Vector3.forward);
        Vector3 StrafeMovement = Input.GetAxis("Horizontal") * (moveSpeed / 2) * Time.deltaTime * transform.TransformDirection(Vector3.right);

        characterController.SimpleMove(fowardMovement);
        characterController.SimpleMove(StrafeMovement);


        


        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
        mainCamera.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0));
    }

}
