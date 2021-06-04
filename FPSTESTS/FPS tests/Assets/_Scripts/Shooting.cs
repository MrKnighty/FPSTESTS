using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] enum FireMode {SingleShot, MultiShot} // this will change the charateristics of the guns
    public GameObject bullet; // what bullet the gun should fire

    public bool canShoot = true; // if this is false, the player cannot shoot

    public float fireDelay; //the delay between shots
    public float reloadSpeed; // how long it takes to reload

    public AudioClip gunShot;
    public AudioClip reload;

    AudioSource source;
     Animator animator;


    public Transform muzzlePoint; // where the bullets spawn

    public int currentAmmo; // how much remeaning ammo the player can shoot before reloading
    public int maxLoadedAmmo; // the max amount of reloaded ammo
    public int currentAmmoPool; // the current amount of spare ammo
    public int MaxAmmoPool; // the max spare ammo the player can have
    public int damage; // how much damage the bullet will do on collison with the target

    public bool reloading; // if the weapon is being currently reloaded

    public Text ammoText; //where the current ammo is being displayed
    GameManager gm;
    
    public ParticleSystem psBulletCasing;
    public ParticleSystem psMuzzleFlash;

    public bool useRecoil; // if recoil will be used for this gun
    public float recoilAmount; // by what magnatude should the recoil effect the camera

    public GameObject cam;

    [SerializeField] FireMode _fireMode;

    public float pelletAmount; // how many bullets to fire in multishot mode
    public float smallPelletOffset;
    public float largePelletOffset; // theese determine by how innacurate the spawned multishotbullets will be
    

    private void Start()
    {
        currentAmmo = maxLoadedAmmo; //initiate the current ammo variable with the max ammo.
        animator = gameObject.GetComponent<Animator>();
        source = gameObject.GetComponent<AudioSource>();
        gm = Object.FindObjectOfType<GameManager>();
        source.volume = PlayerPrefs.GetFloat("AudioLevel");
        currentAmmoPool = MaxAmmoPool; //start the player with max ammo
        
    }
    private void OnEnable()
    {
        ammoText.text = ("Ammo:" + currentAmmo + "/" + maxLoadedAmmo); // when swicting back to this weapon, update the ammo text so it displays the correct ammount
    }
    private void OnDisable()
    {
        ammoText.text = (""); // clear the text when diseqipping the weapon
        gameObject.GetComponent<Animator>().StopPlayback();
        StopAllCoroutines(); // stop reloading when the player switches weapons
        reloading = false; // set reloading to false, so that hte gun doesnt keep reloading when disabled
    }

    
    

    private void Update()
    {
        switch(_fireMode)
        {

            case FireMode.SingleShot:
            {
                if (Input.GetMouseButton(0) && canShoot && currentAmmo! >= 1 && !reloading && gm.acceptInput) 
                        {
                            Camera cam = Camera.main;
                            RaycastHit hit;

                            GameObject spawnedBullet = Instantiate(bullet, muzzlePoint.transform.position, muzzlePoint.transform.rotation); //spawn the bullet at the muzzle point
                            spawnedBullet.gameObject.GetComponent<Damager>().ModifyDamage(damage); //modify the damage of the spawned bullet
                            spawnedBullet.transform.parent = null; // deparent the bullet, so that the bullet does not get destroied, one the enenemy dies

                            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity)) 
                            {                                                                                                       // this will point the bullet towards wherever the center of the screen is pointing at
                                spawnedBullet.transform.LookAt(new Vector3(hit.point.x, hit.point.y, hit.point.z));                 // this makes it look like the bullet is coming out of the barrel, while also moving the bullet towards the screen
//                                print(hit.transform.tag);                                                                                            
                            }
                        
                            
                            animator.SetTrigger("Shoot"); //play the animation event shoot
                            source.PlayOneShot(gunShot); // play the sound gunshot
                            canShoot = false; // to add a firerate, we disable shooting after the player has shoot one bullet, then we call a funtion that resets the shooting after x secconds
                            Invoke("ResetShoot", fireDelay); // to make a fire rate, invoke a function that will reset canshoot back to true
                            currentAmmo--; //since theese guns have a limited mag size, remove 1;
                            ammoText.text = ("Ammo:" + currentAmmo + "/" + currentAmmoPool); // after all caculations are done, display the current ammo
                            psBulletCasing.Play();
                            psMuzzleFlash.Play();

                            if(useRecoil) Recoil();
                        }
                        else if (currentAmmo <=0 && !reloading  && currentAmmo != maxLoadedAmmo && currentAmmoPool != 0 || Input.GetKeyDown("r") && !reloading && currentAmmo != maxLoadedAmmo && currentAmmoPool != 0) // if the user hits r, or if the player runs out of ammo invoke the reaload function
                        {
                            StartCoroutine("Reload");
                            reloading = true; // set realoding to true since, we dont want the player to shoot while reloading
                            ammoText.text = ("RELOADING");
                            animator.SetTrigger("Reload");
                            source.PlayOneShot(reload);
                        }

                break;
            }

            case FireMode.MultiShot: // this is almost the same at the above function, but spawns multible pellets, and randomises the position;
            {
                Camera cam = Camera.main;
                RaycastHit hit;


               if (Input.GetMouseButtonDown(0) && canShoot && currentAmmo ! >= 1 && !reloading && gm.acceptInput)
               {
                   for(int i = 0; i < pelletAmount; i++)
                   {
                       GameObject spawnedBullet = Instantiate(bullet, muzzlePoint.transform.position, muzzlePoint.transform.rotation); //spawn the bullet at the muzzle point
                       spawnedBullet.gameObject.GetComponent<Damager>().ModifyDamage(damage); //modify the damage of the spawned bullet
                       spawnedBullet.transform.parent = null; // deparent the bullet, so that the bullet does not get destroied, one the enenemy dies

                            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity)) 
                            {   
                                
                                if(i <= pelletAmount*.5f) // shoot half of the bullets accurately so that atlest some of the bullets are garanteed to hit, but heavly radomize the other half so it cant be used at long distances
                                {
                                    spawnedBullet.transform.LookAt
                                    (new Vector3(hit.point.x + Random.Range(-smallPelletOffset, smallPelletOffset), hit.point.y + Random.Range(-smallPelletOffset, smallPelletOffset), hit.point.z + Random.Range(-smallPelletOffset, smallPelletOffset)));                 
                                }
                                else
                                {
                                    spawnedBullet.transform.LookAt
                                    (new Vector3(hit.point.x + Random.Range(-largePelletOffset, largePelletOffset), hit.point.y + Random.Range(-largePelletOffset, largePelletOffset), hit.point.z+ Random.Range(-largePelletOffset, largePelletOffset)));
                                }
                               // print(hit.transform.tag);                                                                                            
                            }

                       print("FiredPellet");
                   }
                            animator.SetTrigger("Shoot"); //play the animation event shoot
                            source.PlayOneShot(gunShot); // play the sound gunshot
                            canShoot = false; // to add a firerate, we disable shooting after the player has shoot one bullet, then we call a funtion that resets the shooting after x secconds
                            Invoke("ResetShoot", fireDelay); // to make a fire rate, invoke a function that will reset canshoot back to true
                            currentAmmo--; //since theese guns have a limited mag size, remove 1;
                            ammoText.text = ("Ammo:" + currentAmmo + "/" + currentAmmoPool); // after all caculations are done, display the current ammo
                            psBulletCasing.Play();
                            psMuzzleFlash.Play();
                            if(useRecoil) Recoil();
               }
                  else if (currentAmmo <=0 && !reloading  && currentAmmo != maxLoadedAmmo && currentAmmoPool != 0|| Input.GetKeyDown("r") && !reloading && currentAmmo != maxLoadedAmmo && currentAmmoPool != 0) // if the user hits r, or if the player runs out of ammo invoke the reaload function
                        {
                            StartCoroutine("Reload");
                            reloading = true; // set realoding to true since, we dont want the player to shoot while reloading
                            ammoText.text = ("RELOADING");
                            animator.SetTrigger("Reload");
                            source.PlayOneShot(reload);
                        }


                break;
            }


        }

    }

    void Recoil()
    {
        cam.transform.Rotate(recoilAmount, Random.Range(-recoilAmount / 2, recoilAmount /2), 0); // rotate the camera on the x unsing the recoil amount, then randomly choose a number for the y roation
    }

    void ResetShoot()
    {
        canShoot = true;
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmoPool += currentAmmo; // if there are still bullets in the gun, add them back to the ammo pool;
        currentAmmo = Mathf.Clamp(currentAmmoPool, 0, maxLoadedAmmo); // clamp the valuse, so that if there are less then the mag amount in the reserve, you dont get extra for free
        currentAmmoPool -= Mathf.Clamp(currentAmmoPool, 0, maxLoadedAmmo); // remove the amount from the pool that you just loaded into the gun
        //currentAmmo = maxLoadedAmmo; // set the current ammo back to the max
        reloading = false; // set this to false so the player can start shooting again.
        ammoText.text = ("Ammo:" + currentAmmo + "/" + currentAmmoPool); // update ammo counter ui back to max
        
    }
}
