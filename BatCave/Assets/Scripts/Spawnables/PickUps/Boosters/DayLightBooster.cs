using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayLightBooster : Powerup
{
    public float DayTimeUpTime;
    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            spriteRenderer.enabled = false;
            EventManager.TriggerEvent(PowerupEvents.PLAYER_LIGHT_PICKUP, DayTimeUpTime);
        }
        if (col.gameObject.tag == "CleanUp") {
            spriteRenderer.enabled = true;
        }
    }
}
