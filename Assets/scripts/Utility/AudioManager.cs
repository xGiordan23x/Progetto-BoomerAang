using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public GameObject prefabEmpty;
   
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("trovato gia un AudioManager nella scena");
        }
        else
        {
            instance = this;
            
        }
    }


    public void PlayAudioClip(AudioClip clipToPLay)
    {
        GameObject temp = Instantiate(prefabEmpty);
        temp.GetComponent<AudioSource>().clip = clipToPLay;
        //temp.AddComponent<AudioSource>().PlayOneShot(clipToPLay);
        temp.GetComponent<AudioSource>().Play();
        Destroy(temp,clipToPLay.length);
    }

}
