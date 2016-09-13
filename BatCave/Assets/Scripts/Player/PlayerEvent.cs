using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerEvent : MonoBehaviour {
    public PlayerResources playerResource;
    public PlayerControls playerControls;

    void OnEnable() {
        EventManager.StartListening(EventTypes.FLY_PICK_UP, flyPickedUp);
        EventManager.StartListening(EventTypes.ECHO_USED_RESOURCES, echoUsed);
        EventManager.StartListening(EventTypes.PLAYER_SHIELD_PICKUP, shieldActive);
        EventManager.StartListening(EventTypes.PLAYER_SHIELD_ENDED, shieldEnded);
    }

    void OnDisable() {
        EventManager.StopListening(EventTypes.FLY_PICK_UP, flyPickedUp);
        EventManager.StopListening(EventTypes.ECHO_USED_RESOURCES, echoUsed);
        EventManager.StopListening(EventTypes.PLAYER_SHIELD_PICKUP, shieldActive);
        EventManager.StopListening(EventTypes.PLAYER_SHIELD_ENDED, shieldEnded);
    }

    void flyPickedUp() {
        playerResource.addStamina(playerResource.batResourcePickup);
        SaveLoadController.GetInstance().GetEndlessSession().AddResourcesGathered(1);
    }

    void echoUsed() {
        playerResource.removeStamina(playerResource.echoCost);
        playerResource.echoUsed();
    }

    void shieldActive() {
        playerControls.SetShield(true);
    }

    void shieldEnded() {
        playerControls.SetShield(false);
    }
}
