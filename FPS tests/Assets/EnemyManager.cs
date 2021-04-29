using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;

    public void PlayerDeathEvent()
    {
        foreach (GameObject enemies in enemies)
        {
                enemies.GetComponent<EnemyController>().playerDead = true;
     
        }



    }


}
