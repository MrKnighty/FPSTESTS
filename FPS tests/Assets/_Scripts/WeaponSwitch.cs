using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
   public GameObject[] guns;
    private void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            foreach (GameObject guns in guns)
            {
                guns.gameObject.SetActive(false);
            }

            guns[0].gameObject.SetActive(true);
        }
        if (Input.GetKeyDown("2"))
        {
            foreach (GameObject guns in guns)
            {
                guns.gameObject.SetActive(false);
            }

            guns[1].gameObject.SetActive(true);

            
        }


    }

}
