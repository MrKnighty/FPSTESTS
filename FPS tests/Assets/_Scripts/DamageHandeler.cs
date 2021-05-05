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

    bool isPlayer;

   // public Text hpText;

    private void Start()
    {
        currentHealth = MaxHealth;
        if(gameObject.tag == "Player") isPlayer = true;
  //      if(isPlayer) hpText.text = ("Health: " + currentHealth); 
        waveSpawner = Object.FindObjectOfType<EnemyWaveSpawner>();
        

    }
    public void DoDamage(float damage)
    {
        currentHealth -= damage;
        if(gameObject.tag == "Enemy") gameObject.GetComponent<EnemyController>().isAgroo = true; //if an enemy ever takes damage, when not agroo, set them to agroo
        

        if(currentHealth <= 0 && !isPlayer)
        {
            waveSpawner.EnemyDefeated();
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
            print (currentHealth / MaxHealth);
            healthBar.fillAmount = currentHealth / MaxHealth; //this immage uses the fill horisontialy function, and this will slide it along
    }

    
}
