using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntercatableObject : MonoBehaviour
{

    public GameObject[] animateObjects;
    public float spawnDelay;
    public bool triggerWave;
    public int waveID;
     void OnTriggerStay(Collider other) 
    {
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine("StartAnimations");
            if(triggerWave) Object.FindObjectOfType<NewWaveManager>().StartWave(waveID);
        }
    }

    IEnumerator StartAnimations()
    {
        foreach(GameObject objects in animateObjects)
        {
            objects.GetComponent<Animator>().Play("Open");
            objects.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
