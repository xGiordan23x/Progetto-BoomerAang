using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class MainMenu : MonoBehaviour
{
    public PlayableDirector timelineIntro;
    public GameObject blackScreen;
    public GameObject introImage;

    private void Start()
    {
        blackScreen.SetActive(false);
        introImage.SetActive(false);
    }
    public void StartGame()
    {
       // Play animazione o TimeLine
     blackScreen.SetActive(true);
     introImage.SetActive(true);
     Cursor.visible = false;
     Cursor.lockState = CursorLockMode.Locked;
     timelineIntro.Play();

    }

    public void ContinueGame()
    {
        GameManager.instance.LoadSavedRoom();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    

    public void StartLevel1()
    {
        GameManager.instance.LoadNextRoom();
    }
}
