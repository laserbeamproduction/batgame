using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenShaker : MonoBehaviour {
    
    private float shakeY = 0;
    private float shakeYSpeed = 0.8f;
    private float resetShakeY = 0.8f;
    

    void Start() {
        EventManager.StartListening(EventTypes.PLAYER_TAKES_DAMAGE, OnPlayerTakesDamage);
    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.PLAYER_TAKES_DAMAGE, OnPlayerTakesDamage);
    }

    void OnPlayerTakesDamage(Dictionary<string, object> arg0) {
        Shake();
    }

    void Shake() {
        shakeY = resetShakeY;
    }

    void Update() {
        Vector2 newPosition = new Vector2(0, shakeY);
        if (shakeY < 0) {
            shakeY *= shakeYSpeed;
        }
        shakeY = -shakeY;
        transform.Translate(newPosition, Space.Self);
    }
}
