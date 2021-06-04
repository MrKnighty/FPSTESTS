using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    public GameObject[] objectsToAnimate;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            print("Pressure Plate Hit");
            foreach(GameObject objects in objectsToAnimate)
            {
                objects.GetComponent<Animator>().Play("Open");
                objects.GetComponent<AudioSource>().Play();
            }

            Destroy(gameObject);
        }
    }
}
