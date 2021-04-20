using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandeler : MonoBehaviour
{

    public float MaxHealth;
    public float currentHealth;


    private void OnCollisionEnter(Collision collision)
    {
      if(collision.transform.tag == "Bullet")
        {
            currentHealth--;
            if(currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
