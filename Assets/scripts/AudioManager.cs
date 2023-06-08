using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    AudioSource audioSource;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("trovato gia un AudioManager nella scena");
        }
        else
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
    }


    public void PlayAduioClip(AudioClip clipToPLay)
    {
        audioSource.clip = clipToPLay;
        audioSource.Play();
        Invoke(nameof(RemoveClip),clipToPLay.length);
        
    }

    private void RemoveClip()
    {
       audioSource.clip = null;
    }
}
