using UnityEngine;
using System.Collections;
using System;

public class GameItem : MonoBehaviour {

    [Range(0f, 100f)]
    public float spawnChance;

    [Range(1, 5)]
    public int laneWeight;

    public int stageLevel;

    public Sprite woods;
    public Sprite purpleCave; 

    private bool isAvailable = true;
    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventManager.StartListening(EventTypes.CHANGE_ENVIRONMENT, OnEnviromentChanged);
    }
    
    void OnDestroy() {
        EventManager.StopListening(EventTypes.CHANGE_ENVIRONMENT, OnEnviromentChanged);
    }

    public bool IsAvailable() {
        return this.isAvailable;
    }

    public void SetAvailable(bool available) {
        this.isAvailable = available;
    }

    public int GetStageLevel() {
        return this.stageLevel;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "CleanUp") {
            SetAvailable(true);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "CleanUp") {
            SetAvailable(true);
        }
    }

    void OnEnviromentChanged(object type) {
        string enviroment = (string)type;
        switch (enviroment) {
            case EnvironmentTypes.WOODS:
                spriteRenderer.sprite = woods;
                break;
            case EnvironmentTypes.PURPLE_CAVE:
                spriteRenderer.sprite = purpleCave;
                break;
            default:
                Debug.LogError("Enviroment type " + enviroment + " not found!");
                break;
        }
    }
}
