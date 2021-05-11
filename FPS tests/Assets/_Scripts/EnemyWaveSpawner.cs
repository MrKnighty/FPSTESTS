using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveInfo // this is all in a class, so i can create an array of all of theese variables, to have multiple waves
{
    public GameObject[] spawnPoints; //where the enemies will spawn
    public GameObject[] enemies; //what enemies to spawn
    public bool doSeccondWave; // if we will spawn a seccond wave

    public GameObject[] spawnPoints2; // seccond waves's spawn pouints
     public GameObject[] enemies2; // seccond waves enemies spawnpoints
    
    public bool animateGameObjects; //if we will call an animation in gameobjectstoanimate
    public GameObject[] gameObjectsToAnimate; // what objects to animate

    public bool animateGameObjects2; 
    public GameObject[] gameObjectsToAnimate2; 

}
public class EnemyWaveSpawner : MonoBehaviour
{
    public WaveInfo[] waves; 

    int currentWave = -1; // this is the current lengh of the wave, its negetive since this is used with an array, and i dont want to start with 1
     int completedWaves = -1; //how many waves the player has completed, this will be kept between level loads
    public int remaingenemies = 0; // how many enemies remain in the wave
    public bool doneSeccondWave; // if the seccond wave has been completed
  
  
    public void StartWave()
    {
        int i;
        i = 0;
        currentWave += 1; //advence the currnet wave
        //if(currentWave > waves.Length) currentWave--; // this is fail safe, if the player somehow triggers the wave trigger twice
        remaingenemies = waves[currentWave].enemies.Length; //set the remaining enemies, to the current enemie array lenght 
        foreach (GameObject enemies in waves[currentWave].enemies)
        {
            Instantiate(waves[currentWave].enemies[i], waves[currentWave].spawnPoints[i].gameObject.transform.position, waves[currentWave].spawnPoints[i].transform.rotation); 
            i ++;
            //this will spawn the enemies, in enemies, at the checkpoints in the array;
        }
        if(waves[currentWave].animateGameObjects) // if animategameobjects is true, then play the intro animation in all of the gameobjects in the array;
        {

            foreach (GameObject objects in waves[currentWave].gameObjectsToAnimate)
            {
                objects.GetComponent<Animator>().Play("Intro");

            }



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
        if(waves[currentWave].animateGameObjects2) // if animategameobjects is true, then play the intro animation in all of the gameobjects in the array;
        {

            foreach (GameObject objects in waves[currentWave].gameObjectsToAnimate2)
            {
                objects.GetComponent<Animator>().Play("Intro");

            }

        }
        doneSeccondWave = true;
    }

     private void Update() 
    {
        if(Input.GetKeyDown("e")) //this is used for easy testing
        {
            StartWave();
        }
       
    }

    public void EnemyDefeated() // this is called by the enemies, when they die
    {
         remaingenemies --; // decrease the enemy counter by one
         print(remaingenemies);
         if(remaingenemies <= 0) // if there are no enemies left chect so see if we need to spawn a seccond wave, if not then do nothing;
         {
             if(waves[currentWave].doSeccondWave &&! doneSeccondWave) 
             {
                 StartSeccondWave();
             }
             completedWaves ++;
         }
         print("enemydefeated");
    }

}

    //TODO:
    // add a finished waves, instead of total waves to fix a expoit where the player can start the 3rd wave, by hittint the 2nd trigger,
