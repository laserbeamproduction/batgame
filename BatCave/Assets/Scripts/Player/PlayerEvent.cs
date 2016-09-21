using UnityEngine;
using System.Collections.Generic;

public class PlayerEvent : MonoBehaviour {
    public PlayerResources playerResource;
    public PlayerControls playerControls;

    void OnEnable() {
        EventManager.StartListening(EventTypes.HEALTH_PICKED_UP, healthPickedUp);
        EventManager.StartListening(EventTypes.ECHO_USED_RESOURCES, echoUsed);
    }

    void OnDisable() {
        EventManager.StopListening(EventTypes.HEALTH_PICKED_UP, healthPickedUp);
        EventManager.StopListening(EventTypes.ECHO_USED_RESOURCES, echoUsed);
    }

    void healthPickedUp(object arg0) {
        playerResource.addHealth(playerResource.healthPickupAmount);
        SaveLoadController.GetInstance().GetEndlessSession().AddResourcesGathered(1);
    }

    void echoUsed(object arg0) {
        playerResource.echoUsed();
    }
}
