using System;

[Serializable]
public class PlayerSave : SaveObject {

    private float highscore;
    private float lastSessionScore;
    private float totalDistanceFlown;
    private int totalGamesPLayed;

    public PlayerSave() { }

    /**
        HIGHSCORE FUNCTIONS    
    */

    public float GetHighscore() {
        return this.highscore;
    }

    public void SetHighscore(float score) {
        this.highscore = score;
    }

    public float GetLastSessionScore() {
        return this.lastSessionScore;
    }

    public void SetLastSessionScore(float score) {
        this.lastSessionScore = score;
    }

    /**
        DISTANCE FLOWN FUNCTIONS    
    */

    public float GetTotalDistanceFlown() {
        return this.totalDistanceFlown;
    }

    public void AddTotalDistanceFlown(float distance) {
        this.totalDistanceFlown += distance;
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
