using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public List<SaveObject> GetSaveObjects() {
        return this.saveObjects;
    }
    
}
