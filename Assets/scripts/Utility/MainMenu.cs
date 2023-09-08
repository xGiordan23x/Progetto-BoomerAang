using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class MainMenu : MonoBehaviour
{
    
   
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
