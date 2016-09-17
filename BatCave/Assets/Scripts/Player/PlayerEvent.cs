using UnityEngine;
using System.Collections.Generic;

public class PlayerEvent : MonoBehaviour {
    public PlayerResources playerResource;
    public PlayerControls playerControls;

    void OnEnable() {
        EventManager.StartListening(EventTypes.HEALTH_PICKED_UP, healthPickedUp);
        EventManager.StartListening(EventTypes.ECHO_USED_RESOURCES, echoUsed);
        EventManager.StartListening(EventTypes.PLAYER_SHIELD_PICKUP, shieldActive);
        EventManager.StartListening(EventTypes.PLAYER_SHIELD_ENDED, shieldEnded);
    }

    void OnDisable() {
        EventManager.StopListening(EventTypes.HEALTH_PICKED_UP, healthPickedUp);
        EventManager.StopListening(EventTypes.ECHO_USED_RESOURCES, echoUsed);
        EventManager.StopListening(EventTypes.PLAYER_SHIELD_PICKUP, shieldActive);
        EventManager.StopListening(EventTypes.PLAYER_SHIELD_ENDED, shieldEnded);
    }

    void healthPickedUp(object arg0) {
        playerResource.addHealth(playerResource.healthPickupAmount);
        SaveLoadController.GetInstance().GetEndlessSession().AddResourcesGathered(1);
    }

    void echoUsed(object arg0) {
        playerResource.echoUsed();
    }

    void shieldActive(object arg0) {
        playerControls.SetShield(true);
    }

    void shieldEnded(object arg0) {
        playerControls.SetShield(false);
    }
}
