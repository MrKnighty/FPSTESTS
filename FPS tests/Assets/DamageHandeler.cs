using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandeler : MonoBehaviour
{

    public float MaxHealth; // the health this will start with, and the maximum
    public float currentHealth; // the current health of this, when it hits zero the death sequence will play

    bool isPlayer;

    private void Start()
    {
        currentHealth = MaxHealth;
    }
    public void DoDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Destroy(this);
        }
    }
}
