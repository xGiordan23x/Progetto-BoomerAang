using UnityEngine;

[System.Serializable]

public class Dialogue
{
    public string name;

    // Attributo per editare una stringa permettendo alla propria area di testo di
    // divenire più grande e aumentare automaticamente all'ammontare delle frasi
    [TextArea(3, 10)]

    public string[] sentences;
}