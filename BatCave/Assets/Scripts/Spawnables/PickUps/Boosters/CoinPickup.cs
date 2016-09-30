using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinPickup : Powerup {

    public int coinRewardAmount;
    public Text coinAmountText;
    private int currentCoins;
    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coinAmountText.text = "0";
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            spriteRenderer.enabled = false;
            currentCoins += coinRewardAmount;
            coinAmountText.text = currentCoins.ToString();
            EventManager.TriggerEvent(PowerupEvents.PLAYER_COIN_PICKUP, coinRewardAmount);
        }
        if (col.gameObject.tag == "CleanUp") {
            spriteRenderer.enabled = true;
        }
    }
}
