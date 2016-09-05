using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerEvent : MonoBehaviour {
    public PlayerResources playerResource;
    public PlayerControls playerControls;

    void OnEnable() {
        EventManager.StartListening(EventTypes.FLY_PICK_UP, flyPickedUp);
        EventManager.StartListening(EventTypes.ECHO_USED, echoUsed);
        EventManager.StartListening(EventTypes.SHAPE_SHIFT, shapeShift);
    }

    void OnDisable() {
        EventManager.StopListening(EventTypes.FLY_PICK_UP, flyPickedUp);
        EventManager.StopListening(EventTypes.ECHO_USED, echoUsed);
        EventManager.StopListening(EventTypes.SHAPE_SHIFT, shapeShift);
    }

    void flyPickedUp() {
        playerResource.addStamina(50);
    }

    void echoUsed() {
        playerResource.removeStamina(50);
    }

    void shapeShift() {
        playerControls.ShapeShift();
    }
}
