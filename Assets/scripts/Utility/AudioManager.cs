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
    public AudioClip clipOSTLevel_1;
   

   
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
        temp.GetComponent<AudioSource>().Play();
        Destroy(temp,clipToPLay.length);
    }

    public void PlayOSTLevel_1()
    {
        GameObject temp = Instantiate(prefabEmpty);
        temp.gameObject.name = "shfefejhui";
        temp.GetComponent<AudioSource>().clip = clipOSTLevel_1;
        temp.GetComponent<AudioSource>().loop= true;
        temp.GetComponent<AudioSource>().Play();

       
    }

}
