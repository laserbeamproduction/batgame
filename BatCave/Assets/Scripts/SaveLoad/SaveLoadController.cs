using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Manager for saving and loading game data.
/// </summary>
public class SaveLoadController {

    private static SaveLoadController instance;

    private List<SaveObject> saveObjects;

    public SaveLoadController() {
        saveObjects = new List<SaveObject>();
    }

    public static SaveLoadController GetInstance() {
        if (instance == null)
            instance = new SaveLoadController();
        return instance;
    }

    /// <summary>
    /// Converts the list of save objects to an array of bytes used for storing
    /// </summary>
    /// <returns></returns>
    public byte[] CreateSaveObject() {
        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream()) {
            bf.Serialize(ms, saveObjects);
            return ms.ToArray();
        }
    }

    /// <summary>
    /// Converts a save file byte array to usable save objects list
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    private void RestoreSave(byte[] bytes) {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(bytes, 0, bytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        saveObjects = (List<SaveObject>)binForm.Deserialize(memStream);
    }
}
