using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damage;

    public bool destroyOnDamage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<DamageHandeler>()) //check if collision has the damage handler to see if it can damage it.
        {
            collision.gameObject.GetComponent<DamageHandeler>().DoDamage(damage);

            if(destroyOnDamage) // destroy object after damage has been done if destroy on damage is true
            {
                Destroy(this);
            }
        }
        
    }
}
// this script can be put on anything that does damage, traps, bullets.