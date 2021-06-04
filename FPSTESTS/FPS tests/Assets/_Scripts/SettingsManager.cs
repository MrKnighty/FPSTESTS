using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    
    public static float mouseSensetiviy;
    static float fovLevel = 90f;

    private void Start() 
    {
     
        AudioSource[] sources = Object.FindObjectsOfType<AudioSource>();
        foreach(AudioSource source in sources)
        {
            source.volume = PlayerPrefs.GetFloat("AudioLevel");
        }
        
        Camera.main.fieldOfView = PlayerPrefs.GetFloat("FovLevel");


        if(PlayerPrefs.HasKey("AudioLevel"))
        {
            print("Loading AudioLevel");
        }
        else
        {
            PlayerPrefs.SetFloat("AudioLevel", 0.5f);
        }

        if(PlayerPrefs.HasKey("FovLevel"))
        {
            print("Loading FovLevel");
        }
        else
        {
            PlayerPrefs.SetFloat("FovLevel", 80);
        }
    }
    
    public void ModifyAudioLevel(float level)
    {
        PlayerPrefs.SetFloat("AudioLevel", level);
    }
    public void ModifyFovLevel(float level)
    {
        PlayerPrefs.SetFloat("FovLevel", level);
    }

     void Update() {
        if(Input.GetKeyDown(KeyCode.F10)) PlayerPrefs.DeleteAll();
        
        
    }
}
