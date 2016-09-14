using UnityEngine;
using System.Collections;

public class PlayerShieldController : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        EventManager.StartListening(EventTypes.PLAYER_SHIELD_PICKUP, OnShieldActivated);
        EventManager.StartListening(EventTypes.PLAYER_SHIELD_ENDED, OnsShieldEnded);
    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.PLAYER_SHIELD_PICKUP, OnShieldActivated);
        EventManager.StopListening(EventTypes.PLAYER_SHIELD_ENDED, OnsShieldEnded);
    }

    void OnShieldActivated() {
        spriteRenderer.enabled = true;
    }

    void OnsShieldEnded() {
        spriteRenderer.enabled = false;
    }
}
