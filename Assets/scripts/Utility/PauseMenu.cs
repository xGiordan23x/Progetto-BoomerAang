using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, ISubscriber
{
    public static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject dialogueBoxUI;
    private Player player;
    private bool inDialogue;
    bool inMainMenu = true;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        PubSub.Instance.RegisteredSubscriber(nameof(PauseMenu), this);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&inMainMenu)
        {
            if (GameIsPaused)
            {

                
                Resume();
                


            }
            else
            {
              
                Pause();
                

            }
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        player.enabled = true;
    }

    private void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // se in dialogo chiudo dialogo
       
        if(inDialogue)
        {
          PubSub.Instance.SendMessageSubscriber(nameof(DialogueManager), this);
        }


        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        player.enabled = false;

    }

    public void Save()
    {
        //quando si potrà salvare     .Fede
        Debug.Log("Ho salvato");
    }

    public void MenuSelection(GameObject menuUi)
    {
        pauseMenuUI.SetActive(false);
        menuUi.SetActive(true);
        inMainMenu = false;
    }

    public void BackSeletion(GameObject menuUI)
    {
        pauseMenuUI.SetActive(true);
        menuUI.SetActive(false);
        inMainMenu = true;
    }

    public void ReturnMenu()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OnNotify(object content, bool vero = false)
    {
        if(content is DialogueManager && vero)
        {
            inDialogue = true;
        }
        if(content is DialogueManager && !vero)
        {
            inDialogue = false;
        }
    }
   
}
