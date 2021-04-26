using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;

    bool canShoot = true; // if this is false, the player cannot shoot

    public float fireDelay; //the delay between shots
    public float reloadSpeed; // how long it takes to reload

    

     Animator animator;


    public Transform muzzlePoint; // where the bullets spawn

    public int currentAmmo;
    public int maxAmmo;
    public int damage;

    bool reloading;

    public Text ammoText;

    public LayerMask lm;
    

    private void Start()
    {
        currentAmmo = maxAmmo; //initiate the current ammo variable with the max ammo.
        gameObject.SetActive(false); // all guns will start enabled, but after initiating set them for disable.
        animator = gameObject.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        ammoText.text = ("Ammo:" + currentAmmo + "/" + maxAmmo);
    }
    private void OnDisable()
    {
        ammoText.text = ("");
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && canShoot && currentAmmo! >= 1 && !reloading) 
        {
            Camera cam = Camera.main;
            RaycastHit hit;

            GameObject spawnedBullet = Instantiate(bullet, muzzlePoint.transform.position, muzzlePoint.transform.rotation);
            spawnedBullet.gameObject.GetComponent<Damager>().ModifyDamage(damage);

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity)) 
             {                                                                                                       // this will point the bullet towards wherever the center of the screen is pointing at
                 spawnedBullet.transform.LookAt(new Vector3(hit.point.x, hit.point.y, hit.point.z));                 // this makes it look like the bullet is coming out of the barrel, while also moving the bullet towards the screen
                print(hit.transform.tag);                                                                                            
             }
           
            
            animator.Play("Shoot");
            canShoot = false; // to add a firerate, we disable shooting after the player has shoot one bullet, then we call a funtion that resets the shooting after x secconds
            Invoke("ResetShoot", fireDelay);
            currentAmmo--;
            ammoText.text = ("Ammo:" + currentAmmo + "/" + maxAmmo);
        }
        else if (currentAmmo <=0 && !reloading  || Input.GetKeyDown("r") && !reloading) // if the user hits r, or if the player runs out of ammo invoke the reaload function
        {
            Invoke("Reload", reloadSpeed);
            reloading = true; // set realoding to true since, we dont want the player to shoot while reloading
            ammoText.text = ("RELOADING");
            animator.Play("Reload");
        }
    }
    void ResetShoot()
    {
        canShoot = true;
    }
    void Reload()
    {
        currentAmmo = maxAmmo; // set the current ammo back to the max
        reloading = false; // set this to false so the player can start shooting again.
        ammoText.text = ("Ammo:" + currentAmmo + "/" + maxAmmo); // update ammo counter ui back to max
        
    }
}
