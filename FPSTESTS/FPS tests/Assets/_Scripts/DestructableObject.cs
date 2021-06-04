using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{

    public GameObject _particleSystem;
    public bool spawnParticles;

    AudioSource source;
    public bool playAudio;

    private void Start() 
    {
        if(playAudio) source = gameObject.GetComponent<AudioSource>(); //check to see if the object will use audio, 
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.transform.tag == "Bullet")
        {
            GameObject ps;
            source.Play();
            ps = Instantiate(_particleSystem, transform);
            ps.transform.parent = null; // since the object is about to be destroied, deparent it
            Destroy(gameObject);
        }
    }
}
