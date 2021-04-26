using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageHandeler : MonoBehaviour
{

    public float MaxHealth; // the health this will start with, and the maximum
    public float currentHealth; // the current health of this, when it hits zero the death sequence will play

    bool isPlayer;

    public Text hpText;

    private void Start()
    {
        currentHealth = MaxHealth;
        if(isPlayer) hpText.text = ("Health: " + currentHealth); 

    }
    public void DoDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0 && !isPlayer)
        {
            Destroy(this.gameObject);
        }

        if(isPlayer)
        {
            hpText.text = ("Health: " + currentHealth);
        }
    }
}
