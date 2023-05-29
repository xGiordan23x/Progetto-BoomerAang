using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager instance { get; private set; }

    private GameData gameData;

    private List<IDataPersistance> dataPersistanceObject;
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
        dataPersistanceObject = FindAllDataPersistanceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
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

        Debug.Log("Salvata l'index della stanza : " + gameData.roomIndex);
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistances = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistances);
    }


}
