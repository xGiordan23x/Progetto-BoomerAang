using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public enum AudioType
{
    Music,
    Effect
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public GameObject prefabEmpty;

    

    [Header("AudioMixers")]
    [SerializeField] AudioMixerGroup audioMixerMusic;
    [SerializeField] AudioMixerGroup audioMixerEffect;

    [Header("LevelMusic")]
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

        //switch (audioType)            //forse ci serve piu in la,vediamo visto che qua passano solo effetti
        //{
        //    case AudioType.Music:
        //        temp.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerMusic;
        //        break;
        //    case AudioType.Effect:
        //        temp.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerEffect;
        //        break;
        //    default:
        //        break;
        //}

        temp.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerEffect;


        temp.GetComponent<AudioSource>().Play();
        
        Destroy(temp,clipToPLay.length);
    }

    public void PlayOSTLevel_1()
    {
        GameObject temp = Instantiate(prefabEmpty);
        temp.gameObject.name = "shfefejhui";
        temp.GetComponent<AudioSource>().clip = clipOSTLevel_1;
        temp.GetComponent<AudioSource>().loop = true;

        temp.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerMusic;

        temp.GetComponent<AudioSource>().Play();

       
    }

}
