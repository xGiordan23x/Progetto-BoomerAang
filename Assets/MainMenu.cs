using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ContinueGame()
    {
        //quando faro i salvataggi     .fede
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
