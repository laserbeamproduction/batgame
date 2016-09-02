﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerEvent : MonoBehaviour {
    public PlayerResources playerResource;


    void Awake() {

    }

    void OnEnable() {
        EventManager.StartListening(EventTypes.FLY_PICK_UP, flyPickedUp);
        EventManager.StartListening(EventTypes.ECHO_USED, echoUsed);
    }

    void OnDisable() {
        EventManager.StopListening(EventTypes.FLY_PICK_UP, flyPickedUp);
        EventManager.StopListening(EventTypes.ECHO_USED, echoUsed);
    }

    void flyPickedUp() {
        playerResource.addStamina(50);
    }

    void echoUsed() {
        playerResource.removeStamina(50);
    }
}
