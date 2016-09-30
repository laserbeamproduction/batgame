using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinPickup : Powerup {

    public int coinRewardAmount;
    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            spriteRenderer.enabled = false;
            EventManager.TriggerEvent(PowerupEvents.PLAYER_COIN_PICKUP, coinRewardAmount);
        }
        if (col.gameObject.tag == "CleanUp") {
            spriteRenderer.enabled = true;
        }
    }
}
