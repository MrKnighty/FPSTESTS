using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    
    
    public bool health;
    public float healAmount;

    public float rotateSpeed;
     void Update() 
    {
        gameObject.transform.Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0, 0)); // rotate the health on the spot
    
    }
     
     void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            if(health) other.gameObject.GetComponent<DamageHandeler>().Heal(healAmount);
            Destroy(this.gameObject);
        }
    }
}
    

