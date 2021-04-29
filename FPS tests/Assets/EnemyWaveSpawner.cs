using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveInfo
{
    public GameObject[] spawnPoints;
    public GameObject[] enemies;
    public bool doSeccondWave;

    public GameObject[] spawnPoints2;
    public GameObject[] enemies2;
    

}
public class EnemyWaveSpawner : MonoBehaviour
{
    public WaveInfo[] waves;

    public int currentWave = -1;
    public int remaingenemies = 0;
    public bool doneSeccondWave;

    public int test;

    
    public void StartWave()
    {
        int i;
        i = 0;
        currentWave += 1;
        remaingenemies = waves[currentWave].enemies.Length;
        foreach (GameObject enemies in waves[currentWave].enemies)
        {
            Instantiate(waves[currentWave].enemies[i], waves[currentWave].spawnPoints[i].gameObject.transform.position, waves[currentWave].spawnPoints[i].transform.rotation);
            i ++;
            //this will spawn the enemies, in enemies, at the checkpoints in the array;
        }
    }
    public void StartSeccondWave()
    {
        int i;
        i = 0;
        remaingenemies = waves[currentWave].enemies2.Length;
        foreach (GameObject enemies in waves[currentWave].enemies2)
        {
            Instantiate(waves[currentWave].enemies2[i], waves[currentWave].spawnPoints2[i].gameObject.transform.position, waves[currentWave].spawnPoints2[i].transform.rotation);
            i ++;
            // this does the same thing at the top function, but this is used for the seccond wave;
        }
        doneSeccondWave = true;
    }

     private void Update() 
    {
        if(Input.GetKeyDown("e"))
        {
            StartWave();
        }
       
    }

    public void EnemyDefeated()
    {
         remaingenemies --;
         print(remaingenemies);
         if(remaingenemies <= 0)
         {
             if(waves[currentWave].doSeccondWave &&! doneSeccondWave) //chect so see if we need to spawn a seccond wave, if not then advance current wave;
             {
                 StartSeccondWave();
             }
             else
             {
                 
             }
         }
         print("enemydefeated");
    }

}

    
