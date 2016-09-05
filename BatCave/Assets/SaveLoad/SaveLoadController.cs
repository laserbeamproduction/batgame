﻿using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

/// <summary>
/// Manager for saving and loading game data.
/// </summary>
public class SaveLoadController {

    private static SaveLoadController instance;

    private List<SaveObject> saveObjects;

    public SaveLoadController() {
        saveObjects = new List<SaveObject>();

        // init all saveobjects
        OptionsSave os = new OptionsSave();

        // add to list
        saveObjects.Add(os);
    }

    public static SaveLoadController GetInstance() {
        if (instance == null)
            instance = new SaveLoadController();
        return instance;
    }

    public OptionsSave GetOptions() {
        return (OptionsSave)GetSaveObject(typeof(OptionsSave));
    }

    /// <summary>
    /// Generic function to get a save object from the pool.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private SaveObject GetSaveObject(Type type) {
        foreach (SaveObject obj in saveObjects) {
            if (obj.GetType() == type)
                return obj;
        }
        Debug.LogError("Type passed not found. Is it initilized in the constructor?");
        return null;
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
    public void RestoreSave(byte[] bytes) {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(bytes, 0, bytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        saveObjects = (List<SaveObject>)binForm.Deserialize(memStream);
    }
}
