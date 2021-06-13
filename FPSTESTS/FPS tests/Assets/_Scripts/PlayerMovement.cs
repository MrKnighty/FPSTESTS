using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController; // store a refrence of the charatercontroller

    public Transform mainCamera;
    public float jumpHeight; // how high the player can jump
    public float gravity; // how fast the player will fall after hitting the apex of their jump

    Vector3 playerVelocity; // how fast the player is planned to move
    public float moveSpeed; // how fast the player will move
    public float airMovementMultiplyer; // a multiplyer that will determine how much faster/ slower they will move when not toching ground
    public float mouseSens;
    public float footStepInteval;

    static bool hasHitCheckpoint;
    //this is used for storing the position of the hit checkpoint, there are static because we need the info when reloading the scene
    static Vector3 checkpointLocation;
    static bool hasSpawned;
    static float spawnpointHP;
    public float spawnpointGraceHP;
    
    public float coyoteTime; // this is a grace period when the player starts to fall off a platform, put pushes jump a couple frames after falling
    float trueCoyoteTime;
    bool canJump;

    GameManager gm;
        float trueMoveSpeed; // this is the move speed that will be used for caculations, this will change when the player is mid air, so they dont have perfect air controll
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        trueMoveSpeed = moveSpeed;
        gm = Object.FindObjectOfType<GameManager>();

        if(hasHitCheckpoint) 
        {
            Vector3 location = checkpointLocation;
            gameObject.transform.position = location;
            print("spawning at checkpoint:" +  checkpointLocation);
            if(spawnpointHP <= 30) spawnpointHP = spawnpointGraceHP; // this is so the player does not get stuck on a checkpoint if they hit it at an extreamly low hp, meaning they cant progress, so insted cap how low it can go
            gameObject.GetComponent<DamageHandeler>().DoDamage((120 - spawnpointHP));

        }

        // if(!hasSpawned)
        // {
        //     hasSpawned = true;
        //     DontDestroyOnLoad(this.transform);
        // }
        // else
        // {
            
        //     Destroy(gameObject);
        // }
        
    }
     
    

    private void Update()
    {
        
        if(gm.acceptInput) NewMovement();


        if(characterController.isGrounded)
        {
            canJump = true;
            trueCoyoteTime = coyoteTime;
        }
        else
        {
            trueCoyoteTime -= Time.deltaTime;
            if(trueCoyoteTime <= 0)
            {
                canJump = false;
            }
        }
        
        
      
    }


    void NewMovement()
    {
        bool groundedPlayer = characterController.isGrounded;
        if (canJump && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = Input.GetAxis("Vertical") * trueMoveSpeed * Time.deltaTime * transform.TransformDirection(Vector3.forward) + Input.GetAxis("Horizontal") * (trueMoveSpeed / 2) * Time.deltaTime * transform.TransformDirection(Vector3.right);
        characterController.Move(move * moveSpeed);

       

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && canJump)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            transform.parent = null; // this is a failsafe for the moving platfroms if ontriggerexit does not get called;
            canJump = false;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSens, 0));
            mainCamera.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * mouseSens, 0, 0));

        if(!characterController.isGrounded)
        {
            trueMoveSpeed = moveSpeed * airMovementMultiplyer; // add a multipleyer if there midair so aircontroll is not perfect
        }
        else
        {
            trueMoveSpeed = moveSpeed; // when the player is grounded again reset movespeed

        }
    }

    public void SetCheckpoint(Vector3 location)
    {
        hasHitCheckpoint = true;
        checkpointLocation = location;
        print("setcheckpoint called");
        spawnpointHP = gameObject.GetComponent<DamageHandeler>().currentHealth;
    }
    public void resetCheckpoint()
    {
        hasHitCheckpoint = false;
        spawnpointHP = 120;
    }

}
