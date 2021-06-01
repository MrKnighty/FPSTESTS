using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRespawn : MonoBehaviour
{

    public GameObject respawnPoint; // where the player will be respawned
    public int damage;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            other.gameObject.transform.position = respawnPoint.transform.position;
            other.gameObject.GetComponent<DamageHandeler>().DoDamage(damage);
            other.transform.parent = null;
        }
    }
}
