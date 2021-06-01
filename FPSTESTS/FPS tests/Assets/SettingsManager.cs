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
    }
    
    public void ModifyAudioLevel(float level)
    {
        PlayerPrefs.SetFloat("AudioLevel", level);
    }
    public void ModifyFovLevel(float level)
    {
        PlayerPrefs.SetFloat("FovLevel", level);
    }
}
