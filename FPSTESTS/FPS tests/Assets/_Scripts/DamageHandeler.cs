using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageHandeler : MonoBehaviour
{

    public float MaxHealth; // the health this will start with, and the maximum
    public float currentHealth; // the current health of this, when it hits zero the death sequence will play
    
    public GameObject deathUI;
  //  public GameObject enemyManager;
    
    public Image healthBar;

     EnemyWaveSpawner waveSpawner;
     public GameObject[] pickups;
     public bool dropPickup;
     public float pickupDropChance;

     public bool spawnGameObjectOnDeath;
     public GameObject ObjectToSpawn;

    bool isPlayer;
    bool dieing; 

   // public Text hpText;

    private void Start()
    {
        currentHealth = MaxHealth;
        if(gameObject.tag == "Player") isPlayer = true;
        // if(isPlayer) healthBar.fillAmount = currentHealth / MaxHealth;
        waveSpawner = Object.FindObjectOfType<EnemyWaveSpawner>();
        

    }
    

    void Update() 
    {
        
        if(Input.GetKeyDown("k")) DoDamage(50f);
    }
    public void DoDamage(float damage)
    {
        currentHealth -= damage;
        if(gameObject.tag == "Enemy") gameObject.GetComponent<EnemyController>().isAgroo = true; //if an enemy ever takes damage, when not agroo, set them to agroo
        

        if(currentHealth <= 0 && !isPlayer && !dieing) // check to see if this has allready been called, since if the enemy is hit by a multi pellet gun, this will run multiple times;
        {
            dieing = true;
            //waveSpawner.EnemyDefeated();
            GameObject dummy;

            if(spawnGameObjectOnDeath) 
            {
               dummy = Instantiate(ObjectToSpawn, transform); 
               dummy.transform.parent = null;
            }
            
            if(dropPickup) rollPickup();
            gameObject.SetActive(false);
        }
        else if(currentHealth <=0 && isPlayer) // do a special death event for the player, since we need to do extra things besides deleting them
        {
                deathUI.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Object.FindObjectOfType<GameManager>().acceptInput = false;
               // gameObject.GetComponent<WeaponSwitch>().PlayerDeathEvent();;
                //enemyManager.GetComponent<EnemyManager>().PlayerDeathEvent();

            //    gameObject.GetComponent<>


        }
//            print (currentHealth / MaxHealth);
            healthBar.fillAmount = currentHealth / MaxHealth; //this immage uses the fill horisontialy function, and this will slide it along
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        healthBar.fillAmount = currentHealth / MaxHealth;
       // currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
    }

    void rollPickup()
    {
        print("calledPickup");
        if(Random.Range(0, 10) >= pickupDropChance)
        {
           int desidedPickup = Random.Range(0, pickups.Length); 
           GameObject pickup = Instantiate(pickups[desidedPickup], gameObject.transform);
           pickup.transform.parent = null;
           print("randomcalled");
        }
        
        
    }

    
}
