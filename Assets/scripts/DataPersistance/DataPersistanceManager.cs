using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    public static DataPersistanceManager instance { get; private set; }

    private GameData gameData;

    private List<IDataPersistance> dataPersistanceObject;

    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("trovato gia un datapersistancemanager nella scena");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistanceObject = FindAllDataPersistanceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            Debug.Log("salvataggio inesistente");
            NewGame();
        }

        {
            foreach (IDataPersistance dataPersistance in dataPersistanceObject)
            {
                dataPersistance.LoadData(gameData);
            }

            Debug.Log("caricata la scena : " + gameData.roomIndex);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistance dataPersistance in dataPersistanceObject)
        {
            dataPersistance.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);

        Debug.Log("Ho salvato");
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistances = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistances);
    }

    private void OnApplicationQuit()
    {
        //salvo all'uscita del gioco, da cambiare
        //SaveGame();
    }


}
