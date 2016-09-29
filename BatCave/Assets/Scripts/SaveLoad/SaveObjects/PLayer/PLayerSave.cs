using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class PlayerSave : SaveObject {

    private float highscore;
    private int totalCoins;
    private int totalGamesPLayed;
    private List<int> unlockedItems = new List<int>();
    private int activeSkinID;
    private int totalMultiplayerMatchesWon;

    public PlayerSave() {
    }

    public void SetActiveSkinID(int skinID) {
        this.activeSkinID = skinID;
    }

    public int GetActiveSkinID() {
        return this.activeSkinID;
    }

    public List<int> GetUnlockedItems() {
        return this.unlockedItems;
    }

    public void AddUnlockedItem(int unlockedItemID) {
        this.unlockedItems.Add(unlockedItemID);
    }

    public float GetHighscore() {
        return this.highscore;
    }

    public void SetHighscore(float score) {
        this.highscore = score;
    }

    public int GetTotalGamesPlayed() {
        return this.totalGamesPLayed;
    }

    public void AddTotalGamesPlayed(int amount) {
        this.totalGamesPLayed += amount;
    }

    public int GetTotalCoins() {
        return this.totalCoins;
    }

    public void AddTotalCoins(int amount) {
        this.totalCoins += amount;
    }

    public int GetTotalMultiplayerMatchesWon() {
        return this.totalMultiplayerMatchesWon;
    }

    public void AddTotalMultiplayerMatchesWon(int amount) {
        this.totalMultiplayerMatchesWon += amount;
    }
}
