using System;
using UnityEngine;

[Serializable]
public class PlayerSave : SaveObject {

    private float highscore;
    private float currentSessionScore;
    private int totalGamesPLayed;
    private float speed;
    private float amountOfFliesGatheredInRun;

    public PlayerSave() {
        // default speed
        this.speed = 4f;
    }

    /**
        HIGHSCORE FUNCTIONS    
    */

    public float GetHighscore() {
        return this.highscore;
    }

    public void SetHighscore(float score) {
        this.highscore = score;
    }

    public float GetCurrentSessionScore() {
        return this.currentSessionScore;
    }

    public void SetCurrentSessionScore(float score) {
        this.currentSessionScore = score;
    }

    /**
        SPEED
    */

    public float GetSpeed() {
        return this.speed;
    }

    public void SetSpeed(float speed) {
        EventManager.TriggerEvent(EventTypes.PLAYER_SPEED_CHANGED);
        this.speed = speed;
    }
    
    /**
        AMOUNT OF GAMES PLAYED FUNCTIONS    
    */

    public int GetTotalGamesPlayed() {
        return this.totalGamesPLayed;
    }

    public void AddTotalGamesPlayed(int amount) {
        this.totalGamesPLayed += amount;
    }

    /**
        FLIES GATHERED IN ONE ENDLESS RUN
    */

    public int GetAmountOfFliesGatheredInRun() {
        return this.totalGamesPLayed;
    }

    public void AddAmountOfFliesGatheredInRun(float amount) {
        this.amountOfFliesGatheredInRun += amount;
    }

    public void ResetAmountOfFliesGatheredInRun() {
        this.amountOfFliesGatheredInRun = 0;
    }


}
