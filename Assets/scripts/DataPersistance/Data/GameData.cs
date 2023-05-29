using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int roomIndex;

    public GameData()
    {
        this.roomIndex = 0;     //valore di default, ossia il menu
    }
}
