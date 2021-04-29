using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
   public int weaponID;

   void OnTriggerEnter(Collider other)
   {
       if(other.transform.tag == "Player")
       {
        other.gameObject.GetComponent<WeaponSwitch>().UnlockWeapon(weaponID);
        Destroy(gameObject);
       }

   }
}
