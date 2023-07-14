using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.LoadNextRoom();
    }

    public void ContinueGame()
    {
        GameManager.instance.LoadSavedRoom();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
