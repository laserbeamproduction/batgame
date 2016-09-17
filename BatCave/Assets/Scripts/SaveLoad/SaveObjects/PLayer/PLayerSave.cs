using System;
using UnityEngine;

[Serializable]
public class PlayerSave : SaveObject {

    private float highscore;
    private int totalGamesPLayed;
    private float speed;

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

    /**
        SPEED
    */

    public float GetSpeed() {
        return this.speed;
    }

    public void SetSpeed(float speed) {
        EventManager.TriggerEvent(EventTypes.PLAYER_SPEED_CHANGED, null);
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
}
