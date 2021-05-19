using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public float audioLevel;
    static float savedAudioLevel;
    public static float mouseSensetiviy;
    static float fovLevel = 90f;

    private void Start() 
    {
        audioLevel = savedAudioLevel;
        AudioSource[] sources = Object.FindObjectsOfType<AudioSource>();

        foreach(AudioSource sauce in sources)
        {
            sauce.volume = savedAudioLevel;
        }

        Camera.main.fieldOfView = fovLevel;
    }
    
    private void Update() {
        print(savedAudioLevel);
    }

    public void ModifyAudioLevel(float level)
    {
        savedAudioLevel = level;
    }
    public void ModifyFovLevel(float level)
    {
        fovLevel = level;
    }
}
