using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSounds : MonoBehaviour
{

   public float footStepInteval;
     float stepCooldown;
    public AudioClip footsteps;
    int currentSound = 0;
    AudioSource source;
    CharacterController ch;
    private void Start() 
    {
        source = gameObject.GetComponent<AudioSource>();
        ch = gameObject.GetComponent<CharacterController>();
    }
    private void Update() 
    {
         stepCooldown -= Time.deltaTime;
        if(Input.GetAxis("Horizontal") != 0f && stepCooldown < 0f && ch.isGrounded || Input.GetAxis("Vertical") !=0f && stepCooldown < 0f && ch.isGrounded)
        {
            stepCooldown = footStepInteval;
            source.pitch = 1 + Random.Range(-0.2f, 0.2f);
            source.PlayOneShot(footsteps);
            
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        if(hit.gameObject.GetComponent<FootStepSoundData>() != null)
        {
            footsteps = hit.gameObject.GetComponent<FootStepSoundData>().sound;
        }
    }
}
