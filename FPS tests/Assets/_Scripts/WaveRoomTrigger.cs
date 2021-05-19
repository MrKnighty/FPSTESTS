using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRoomTrigger : MonoBehaviour
{
    public int waveID;
    private void OnTriggerEnter(Collider other) 
    
    {
        if (other.transform.tag == "Player")
        {
            Object.FindObjectOfType<EnemyWaveSpawner>().StartWave(waveID);
            Destroy(gameObject);

        }



    }
}
