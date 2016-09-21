using UnityEngine;
using System.Collections;
using System;

public class PowerupController : MonoBehaviour {

    public DayNightCycle dayNightCycle;
    public PlayerControls playerControls;

    bool shieldActive;
    bool speedActive;
    bool lightActive;

    float shieldDuration;
    float speedDuruation;
    float lightDuration;

	// Use this for initialization
	void Start () {
        EventManager.StartListening(PowerupEvents.PLAYER_SHIELD_PICKUP, OnShieldPickedUp);
        EventManager.StartListening(PowerupEvents.PLAYER_SPEED_PICKUP, OnPlayerSpeedPickedUp);
        EventManager.StartListening(PowerupEvents.PLAYER_LIGHT_PICKUP, OnPlayerLightPickedUp);
    }

    void Update() {
        if (shieldActive) {
            if (shieldDuration > 0) {
                shieldDuration -= Time.deltaTime;
            } else {
                shieldActive = false;
                EventManager.TriggerEvent(PowerupEvents.PLAYER_SHIELD_ENDED);
            }
        }

        if (speedActive) {
            if (speedDuruation > 0) {
                speedDuruation -= Time.deltaTime;
            } else {
                speedActive = false;
                dayNightCycle.DecreasePlayerLightRange();
                EventManager.TriggerEvent(PowerupEvents.PLAYER_SPEED_ENDED);
            }
        }

        if (lightActive) {
            if (lightDuration > 0) {
                lightDuration -= Time.deltaTime;
            } else {
                lightActive = false;
                dayNightCycle.setNightTime();
                EventManager.TriggerEvent(PowerupEvents.PLAYER_LIGHT_ENDED);
            }
        }
    }

    private void OnShieldPickedUp(object duration) {
        shieldActive = true;
        shieldDuration = (float)duration;
        playerControls.SetShield(true);
    }

    private void OnPlayerSpeedPickedUp(object duration) {
        speedActive = true;
        speedDuruation = (float) duration;
        playerControls.SetShield(true);
        dayNightCycle.IncreasePlayerLightRange();
    }

    private void OnPlayerLightPickedUp(object arg0) {
        lightActive = true;
        dayNightCycle.setDayTime();
    }

    void OnDestroy() {
        EventManager.StopListening(PowerupEvents.PLAYER_SHIELD_PICKUP, OnShieldPickedUp);
        EventManager.StopListening(PowerupEvents.PLAYER_SPEED_PICKUP, OnPlayerSpeedPickedUp);
        EventManager.StopListening(PowerupEvents.PLAYER_LIGHT_PICKUP, OnPlayerLightPickedUp);
    }
}
