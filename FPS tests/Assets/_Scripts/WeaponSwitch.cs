using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
   public GameObject[] guns;
   public  bool[] unlockedGuns;

   GameManager gm;

   bool dead;

   void Start()
   {
       gm = Object.FindObjectOfType<GameManager>(); //get a refrence to the gamemanger
   } 
    private void Update()
    {
        if (Input.GetKeyDown("1") && unlockedGuns[0] == true && gm.acceptInput) SwitchWeapon(0);  //this will call switchweapon, which will disable the current weapon, then enable the selected weapon
        if (Input.GetKeyDown("2") && unlockedGuns[1] == true && gm.acceptInput) SwitchWeapon(1);
        if (Input.GetKeyDown("3") && unlockedGuns[0] == true && gm.acceptInput) SwitchWeapon(2);
        if (Input.GetKeyDown("4") && unlockedGuns[0] == true && gm.acceptInput) SwitchWeapon(3);
        if (Input.GetKeyDown("5") && unlockedGuns[0] == true && gm.acceptInput) SwitchWeapon(4);
        
    }
    void SwitchWeapon(int input)
    {
        foreach (GameObject guns in guns)
        {
           guns.GetComponent<Animator>().Play("Reload", 0, 0);
           guns.gameObject.SetActive(false);
           
          // guns.gameObject.GetComponent<Shooting>().Disable();
        }
         guns[input].gameObject.SetActive(true);
        // guns[input].gameObject.GetComponent<Shooting>().Enable();
    }

    public void UnlockWeapon(int ID)
    {
        unlockedGuns[ID] = true;

        foreach (GameObject guns in guns)
            {
                guns.GetComponent<Animator>().StopPlayback(); // if you switch guns during the reload anim, it gets stuck on that animation so stop playback so that doesnt happen;
                guns.gameObject.SetActive(false); // once the player collects a new gun, unlock it and allow it to be equped
                
            }

            guns[ID].gameObject.SetActive(true); // also auto equip the new gun
    }
    
    // public void PlayerDeathEvent()
    // {
    //     foreach (GameObject guns in guns)
    //         {
    //             guns.gameObject.SetActive(false); // disable all guns when the player dies
    //             dead = true;
    //         }
    // }



}
