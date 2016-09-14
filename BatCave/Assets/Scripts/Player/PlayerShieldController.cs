using UnityEngine;
using System.Collections;

public class PlayerShieldController : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    public float blinkInterval = 0.5f;
    public bool powerActive;
    public ShieldBooster boosterScript;

    private float blinkIntervalCounter;
    private bool isBlinking;
    private float shieldDuration;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        shieldDuration = boosterScript.shieldDuration;
        EventManager.StartListening(EventTypes.PLAYER_SHIELD_PICKUP, OnShieldActivated);
        EventManager.StartListening(EventTypes.PLAYER_SHIELD_ENDED, OnsShieldEnded);
    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.PLAYER_SHIELD_PICKUP, OnShieldActivated);
        EventManager.StopListening(EventTypes.PLAYER_SHIELD_ENDED, OnsShieldEnded);
    }

    void OnShieldActivated() {
        spriteRenderer.enabled = true;
        powerActive = true;
    }

    void OnsShieldEnded() {
        isBlinking = false;
        blinkIntervalCounter = 0;
        spriteRenderer.enabled = false;
        powerActive = false;
    }


    void Update() {
        if (shieldDuration < 2f && !isBlinking && powerActive) {
            isBlinking = true;
        }

        if (isBlinking) {
            blinkIntervalCounter += Time.deltaTime;
            if (blinkIntervalCounter >= blinkInterval) {
                ToggleSprite();
                blinkIntervalCounter = 0;
            }
        }
    }

    void ToggleSprite() {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }
}
