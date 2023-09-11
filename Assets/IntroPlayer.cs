using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroPlayer : MonoBehaviour
{
    public PlayableDirector timelineIntro;
    public GameObject blackScreen;
    public GameObject introImage;
    public AudioClip introMusicClip;


    private void Start()
    {
        blackScreen.SetActive(false);
        introImage.SetActive(false);
    }

    public void StartIntro()
    {       
        blackScreen.SetActive(true);
        introImage.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        timelineIntro.Play();

    }
    public void PlayIntroClip()
    {        
      AudioManager.instance.PlayAudioClip(introMusicClip);        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
