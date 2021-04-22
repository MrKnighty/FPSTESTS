using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;

    bool canShoot = true;

    public float fireDelay;

    public Transform muzzlePoint; // where the bullets spawn

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            Camera cam = Camera.main;
            RaycastHit hit;

            GameObject spawnedBullet = Instantiate(bullet, muzzlePoint.transform.position, cam.transform.localRotation);

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity)) 
            {                                                                                                       // this will point the bullet towards wherever the center of the screen is pointing at
                spawnedBullet.transform.LookAt(new Vector3(hit.point.x, hit.point.y, hit.point.z));                 // this makes it look like the bullet is coming out of the barrel, while also moving the bullet towards the screen
                    print(hit.transform.tag);                                                                       // only problem with this is that if you shoot nothing, the bullet will glitchout and go in a random direction, but since all levels will be indoors this is "fine"                       
            }
            canShoot = false; // to add a firerate, we disable shooting after the player has shoot one bullet, then we call a funtion that resets the shooting after x secconds
            Invoke("resetShoot", fireDelay);
        }
    }
    void resetShoot()
    {
        canShoot = true;
    }
}
