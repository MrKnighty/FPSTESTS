using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupManager : MonoBehaviour
{
    
    public enum pickupType {Health, Ammo}
    public float healAmount;
    public int ammoAmount;
    public int ammoID;
    public bool rotate;
    public float rotateSpeed;
    [SerializeField] pickupType _pickupType;
     void Update() 
    {
      if(rotate)  gameObject.transform.Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0, 0)); // rotate the health on the spot
    }
     
     void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            
            switch(_pickupType)
            {
                case pickupType.Health:
                {
                    other.gameObject.GetComponent<DamageHandeler>().Heal(healAmount);
                    StartCoroutine("DisplayText", ("Pickuped: " + healAmount + " " + _pickupType));
                    Destroy(this.gameObject);
                    break;
                }

                case pickupType.Ammo:
                {
                    other.GetComponent<WeaponSwitch>().AddAmmo(ammoID, ammoAmount);
                  //  StartCoroutine("DisplayText", ("Pickuped: " + ammoAmount + " Bullets"));
                    
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }


}
    

