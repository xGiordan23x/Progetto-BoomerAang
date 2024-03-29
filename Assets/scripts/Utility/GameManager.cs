using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistance
{
    PauseMenu pauseMenu;

    [Header("Level manager")]   

    public int CurrentRoomIndex = 0;
    [SerializeField] int SaveRoomIndex = 0;
    [SerializeField] int maxIndex;
    [SerializeField] LevelLoader levelLoader;
    Animator transition;
    [SerializeField] float transitionTime = 1f;



    [Header("Inventory manager")]

    private int _chiaviCounter;
    private int _chipCounter;

    public int ChiaviCounter
    {
        get => _chiaviCounter;

        set
        {
            _chiaviCounter = value;
            OnkeyPickUp.Invoke(_chiaviCounter);
        }
    }

    public int ChipCounter
    {
        get => _chipCounter;

        set
        {
            _chipCounter = value;
            OnChipPickUp.Invoke(_chipCounter);
        }
    }

    public delegate void OnKeyPickUpFunction(int value);
    public event OnKeyPickUpFunction OnkeyPickUp;

    public delegate void OnChipPickUpFunction(int value);
    public event OnChipPickUpFunction OnChipPickUp;



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
        maxIndex = SceneManager.sceneCountInBuildSettings - 1;
        CurrentRoomIndex = SceneManager.GetActiveScene().buildIndex;

        if (CurrentRoomIndex == 0)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }




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
            DataPersistanceManager.instance.SaveGame();
            //SceneManager.LoadScene(CurrentRoomIndex);


            StartCoroutine(LoadLevel(CurrentRoomIndex));


        }

    }

    public void LoadSavedRoom()
    {
        StartCoroutine(LoadLevel(SaveRoomIndex));
    }

    public void LoadData(GameData data)
    {
        SaveRoomIndex = data.roomIndex;

    }

    public void SaveData(ref GameData data)
    {
        data.roomIndex = CurrentRoomIndex;
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (transition == null)
        {
            transition = levelLoader.animator;
        }

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
