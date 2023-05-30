using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistance
{
    public int CurrentRoomIndex = 0;
    [SerializeField] int SaveRoomIndex=0;

    [SerializeField] int maxIndex;

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("trovato gia un GameManager nella scena");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        maxIndex = SceneManager.sceneCountInBuildSettings-1;
        CurrentRoomIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextRoom()
    {
        if (CurrentRoomIndex >= maxIndex)
        {
            Debug.LogError("numero livello non valido");
        }
        else
        {
            CurrentRoomIndex++;
            SceneManager.LoadScene(CurrentRoomIndex);
        }

    }

    public void LoadSavedRoom()
    {
        SceneManager.LoadScene(SaveRoomIndex);
    }

    public void LoadData(GameData data)
    {
        SaveRoomIndex= data.roomIndex;
        
    }

    public void SaveData(ref GameData data)
    {
        data.roomIndex = CurrentRoomIndex;
    }
}
