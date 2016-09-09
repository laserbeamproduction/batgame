using System;

/// <summary>
/// This class holds information about a single run in endless mode.
/// </summary>
[Serializable]
public class EndlessSessionSave : SaveObject {

    private float resourcesGathered;
    private float score;
    private float echosTimedGood;
    private float echosTimedExcellent;

    public EndlessSessionSave() {
        
    }

    public void Reset() {
        resourcesGathered = 0f;
        score = 0f;
        echosTimedGood = 0f;
        echosTimedExcellent = 0f;
    }

    public float GetResourcesGathered() {
        return this.resourcesGathered;
    }

    public void AddResourcesGathered(float amount) {
        this.resourcesGathered += amount;
    }

    public float GetScore() {
        return this.score;
    }

    public void SetScore(float value) {
        this.score = value;
    }

    public float GetEchosTimedGood() {
        return this.echosTimedGood;
    }

    public void AddEchosTimedGood(float amount) {
        this.echosTimedGood += amount;
    }

    public float GetEchosTimedExcellent() {
        return this.echosTimedExcellent;
    }

    public void AddEchosTimedExcellent(float amount) {
        this.echosTimedExcellent += amount;
    }
}
