using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damage;

    public bool destroyOnCollide;

    public GameObject particles;
    public bool useParticles; // since this script is used for everything that does damage, not all of it need to spawn particles

    private void OnCollisionEnter(Collision collision)
    {
        DamageHandeler dH;

        if(dH = collision.gameObject.GetComponent<DamageHandeler>()) //check if collision has the damage handler to see if it can damage it.
        {
            collision.gameObject.GetComponent<DamageHandeler>().DoDamage(damage);
            
            
        }

        if (destroyOnCollide) // destroy object after a collsion has been done if true
        {
            Destroy(this.gameObject);
            
        }

        if (useParticles) 
        {
            GameObject spawnedParticles = Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(spawnedParticles, 1f);
        }

    }

    public void ModifyDamage(int damageModifier) // this will be used to change the damage, depending on what weapon the player uses, or a possible power up
    {
        damage = damageModifier;
        
    }
}


// this script can be put on anything that does damage, traps, bullets.