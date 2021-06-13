using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRoomTrigger : MonoBehaviour
{
    public int waveID;
    AudioSource source;
    public bool playSound;

    public AudioClip sound;
    private void Start() {
        source = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) 
    
    {
        if (other.transform.tag == "Player")
        {
            Object.FindObjectOfType<NewWaveManager>().StartWave(waveID);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            if(playSound) source.PlayOneShot(sound);


        }



    }
}
