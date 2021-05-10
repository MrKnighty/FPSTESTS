using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().SetCheckpoint(gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
