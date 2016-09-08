using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerEvent : MonoBehaviour {
    public PlayerResources playerResource;
    public PlayerControls playerControls;

    void OnEnable() {
        EventManager.StartListening(EventTypes.FLY_PICK_UP, flyPickedUp);
        EventManager.StartListening(EventTypes.ECHO_USED, echoUsed);
    }

    void OnDisable() {
        EventManager.StopListening(EventTypes.FLY_PICK_UP, flyPickedUp);
        EventManager.StopListening(EventTypes.ECHO_USED, echoUsed);
    }

    void flyPickedUp() {
        playerResource.addStamina(playerResource.batResourcePickup);
        SaveLoadController.GetInstance().GetPlayer().AddAmountOfFliesGatheredInRun(1);
    }

    void echoUsed() {
        playerResource.removeStamina(playerResource.echoCost);
        playerResource.echoUsed();
    }
}
