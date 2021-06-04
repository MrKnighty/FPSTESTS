using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntercatableObject : MonoBehaviour
{

    public GameObject[] animateObjects;
    public float spawnDelay;
    public bool triggerWave;
    public int waveID;
    GameObject player;
    public AudioClip openSound;
    public int interactionDistance;
    public GameObject intercationText;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update() 
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) <= interactionDistance  && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine("StartAnimations");
            if(triggerWave) Object.FindObjectOfType<NewWaveManager>().StartWave(waveID);
        }
        // if(Vector3.Distance(this.transform.position, player.transform.position) <= interactionDistance)
        // {
        //     intercationText.SetActive(true);
        // }
        // else
        // {
        //     intercationText.SetActive(false);
        // }
    }
     void OnTriggerStay(Collider other) 
    {
        // if(other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        // {
        //     StartCoroutine("StartAnimations");
        //     if(triggerWave) Object.FindObjectOfType<NewWaveManager>().StartWave(waveID);
        // }
    }

    IEnumerator StartAnimations()
    {
        foreach(GameObject objects in animateObjects)
        {
            objects.GetComponent<Animator>().Play("Open");
            objects.GetComponent<AudioSource>().PlayOneShot(openSound);
            yield return new WaitForSeconds(spawnDelay);
            
        }

        gameObject.GetComponent<IntercatableObject>().enabled = false;
    }
}
