using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
   public GameObject[] guns;
   public  bool[] unlockedGuns;

 int currentGun;

   GameManager gm;

   void Start()
   {
       gm = Object.FindObjectOfType<GameManager>(); //get a refrence to the gamemanger
       SwitchWeapon(0); // start the player with the first weapon

   } 
    private void Update()
    {
        if (Input.GetKeyDown("1") && unlockedGuns[0] == true && gm.acceptInput && currentGun != 0) SwitchWeapon(0);  //this will call switchweapon, which will disable the current weapon, then enable the selected weapon
        if (Input.GetKeyDown("2") && unlockedGuns[1] == true && gm.acceptInput && currentGun != 1) SwitchWeapon(1);  // also check if the player is trying to switch to a weapon they have allready equipted;
        if (Input.GetKeyDown("3") && unlockedGuns[0] == true && gm.acceptInput && currentGun != 2) SwitchWeapon(2);
        if (Input.GetKeyDown("4") && unlockedGuns[0] == true && gm.acceptInput && currentGun != 3) SwitchWeapon(3);
       // if (Input.GetKeyDown("5") && unlockedGuns[0] == true && gm.acceptInput && currentGun != 4) SwitchWeapon(4);
        
    }
    void SwitchWeapon(int input)
    {
        guns[currentGun].SetActive(false);
        currentGun = input;
        guns[input].gameObject.SetActive(true);
    }

    public void UnlockWeapon(int ID)
    {
             unlockedGuns[ID] = true;
             guns[currentGun].SetActive(false);
             currentGun = ID;
             guns[ID].gameObject.SetActive(true); // also auto equip the new gun
    }

    public void AddAmmo(int gunID, int ammoAmount)
    {
        guns[gunID].GetComponent<Shooting>().currentAmmoPool += Mathf.Clamp(guns[gunID].GetComponent<Shooting>().currentAmmoPool, 0, 
        guns[gunID].GetComponent<Shooting>().MaxAmmoPool); // clamp the valuse so that the player does not go over their ammo cap
    }
    
 



}
