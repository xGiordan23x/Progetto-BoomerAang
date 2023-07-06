using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] GameObject PauseUI;

    public AudioMixer audioMixer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUI.GetComponent<PauseMenu>().BackSeletion(this.gameObject);
        }
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        Debug.Log("Ho cambiato lo scermo");
    }

    public void SetMainVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {

    }

    public void SetEffectVolume(float volume)
    {

    }


}
