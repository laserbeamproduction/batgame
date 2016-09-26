using UnityEngine;
using System.Collections;
using System;

public class GameItem : MonoBehaviour {

    [Range(0f, 100f)]
    public float spawnChance;

    [Range(1, 5)]
    public int laneWeight;

    public int stageLevel;

    public bool changeSpriteWithEnviroment;
    public Sprite woods;
    public Sprite purpleCave;
    public Sprite iceCave;

    private bool isAvailable = true;
    private SpriteRenderer spriteRenderer;
    private Sprite nextSprite;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventManager.StartListening(EventTypes.TRANSITION_START, OnTransitionStart);
        EventManager.StartListening(EventTypes.TRANSITION_END, OnTransitionEnd);
    }

    void Update() {
        if (isAvailable && nextSprite != null) {
            nextSprite = null;
            spriteRenderer.sprite = nextSprite;
        }
    }

    void OnTransitionEnd(object type) {
        if (!changeSpriteWithEnviroment)
            return;
        Sprite sprite;
        string enviroment = type.ToString();
        switch (enviroment) {
            case EnvironmentTypes.WOODS:
                sprite = woods;
                break;
            case EnvironmentTypes.PURPLE_CAVE:
                sprite = purpleCave;
                break;
            case EnvironmentTypes.ICE_CAVE:
                sprite = iceCave;
                break;
            default:
                Debug.LogError("Enviroment type " + enviroment + " not found!");
                return;
        }

        SetSprite(sprite);
    }

    void OnTransitionStart(object arg0) {
        if (changeSpriteWithEnviroment)
            SetSprite(woods);
    }

    void SetSprite(Sprite sprite) {
        if (isAvailable) {
            spriteRenderer.sprite = sprite;
        } else {
            nextSprite = sprite;
        }
    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.TRANSITION_START, OnTransitionStart);
        EventManager.StopListening(EventTypes.TRANSITION_END, OnTransitionEnd);
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
}
