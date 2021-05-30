using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] enum FireMode {SingleShot, MultiShot}
    public GameObject bullet;

    public bool canShoot = true; // if this is false, the player cannot shoot

    public float fireDelay; //the delay between shots
    public float reloadSpeed; // how long it takes to reload

    public AudioClip gunShot;
    public AudioClip reload;

    AudioSource source;

    
     Animator animator;


    public Transform muzzlePoint; // where the bullets spawn

    public int currentAmmo;
    public int maxAmmo;
    public int damage;

    public bool reloading;

    public Text ammoText;

    public LayerMask lm;
    GameManager gm;
    
    public ParticleSystem psBulletCasing;
    public ParticleSystem psMuzzleFlash;

    public bool useRecoil;
    public float recoilAmount;

    public GameObject cam;

    [SerializeField] FireMode _fireMode;

    public float pelletAmount;
    public float smallPelletOffset = 0.1f;
    public float largePelletOffset = 0.1f;
    

    private void Start()
    {
        currentAmmo = maxAmmo; //initiate the current ammo variable with the max ammo.
        animator = gameObject.GetComponent<Animator>();
        source = gameObject.GetComponent<AudioSource>();
        gm = Object.FindObjectOfType<GameManager>();
        source.volume = PlayerPrefs.GetFloat("AudioLevel");
        
    }
    private void OnEnable()
    {
        ammoText.text = ("Ammo:" + currentAmmo + "/" + maxAmmo); // when swicting back to this weapon, update the ammo text so it displays the correct ammount
    }
    private void OnDisable()
    {
        ammoText.text = (""); // clear the text when diseqipping the weapon
        gameObject.GetComponent<Animator>().StopPlayback();
        StopAllCoroutines(); // stop reloading when the player switches weapons
        reloading = false;
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
                            ammoText.text = ("Ammo:" + currentAmmo + "/" + maxAmmo); // after all caculations are done, display the current ammo
                            psBulletCasing.Play();
                            psMuzzleFlash.Play();

                            if(useRecoil) Recoil();
                        }
                        else if (currentAmmo <=0 && !reloading  && currentAmmo != maxAmmo || Input.GetKeyDown("r") && !reloading && currentAmmo != maxAmmo) // if the user hits r, or if the player runs out of ammo invoke the reaload function
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
                                // this will point the bullet towards wherever the center of the screen is pointing at
                                if(i <= pelletAmount*.5f)
                                {
                                    spawnedBullet.transform.LookAt
                                    (new Vector3(hit.point.x + Random.Range(-smallPelletOffset, smallPelletOffset), hit.point.y + Random.Range(-smallPelletOffset, smallPelletOffset), hit.point.z + Random.Range(-smallPelletOffset, smallPelletOffset)));                 // this makes it look like the bullet is coming out of the barrel, while also moving the bullet towards the screen
                                }
                                else
                                {
                                    spawnedBullet.transform.LookAt
                                    (new Vector3(hit.point.x + Random.Range(-largePelletOffset, largePelletOffset), hit.point.y + Random.Range(-largePelletOffset, largePelletOffset), hit.point.z+ Random.Range(-largePelletOffset, largePelletOffset)));
                                }
                                print(hit.transform.tag);                                                                                            
                            }

                       print("FiredPellet");
                   }
                            animator.SetTrigger("Shoot"); //play the animation event shoot
                            source.PlayOneShot(gunShot); // play the sound gunshot
                            canShoot = false; // to add a firerate, we disable shooting after the player has shoot one bullet, then we call a funtion that resets the shooting after x secconds
                            Invoke("ResetShoot", fireDelay); // to make a fire rate, invoke a function that will reset canshoot back to true
                            currentAmmo--; //since theese guns have a limited mag size, remove 1;
                            ammoText.text = ("Ammo:" + currentAmmo + "/" + maxAmmo); // after all caculations are done, display the current ammo
                            psBulletCasing.Play();
                            psMuzzleFlash.Play();
               }
                  else if (currentAmmo <=0 && !reloading  && currentAmmo != maxAmmo || Input.GetKeyDown("r") && !reloading && currentAmmo != maxAmmo) // if the user hits r, or if the player runs out of ammo invoke the reaload function
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
        cam.transform.Rotate(recoilAmount, Random.Range(-recoilAmount / 2, recoilAmount /2), 0);
    }

    void ResetShoot()
    {
        canShoot = true;
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = maxAmmo; // set the current ammo back to the max
        reloading = false; // set this to false so the player can start shooting again.
        ammoText.text = ("Ammo:" + currentAmmo + "/" + maxAmmo); // update ammo counter ui back to max
        
    }
}
