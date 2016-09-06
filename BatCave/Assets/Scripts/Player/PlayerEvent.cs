using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerEvent : MonoBehaviour {
    public PlayerResources playerResource;
    public PlayerControls playerControls;

    void OnEnable() {
        EventManager.StartListening(EventTypes.FLY_PICK_UP, flyPickedUp);
        EventManager.StartListening(EventTypes.ECHO_USED, echoUsed);
        //EventManager.StartListening(EventTypes.SHAPE_SHIFT, shapeShift);
        //EventManager.StartListening(EventTypes.BLOOD_SENT, bloodSentUsed);
    }

    void OnDisable() {
        EventManager.StopListening(EventTypes.FLY_PICK_UP, flyPickedUp);
        EventManager.StopListening(EventTypes.ECHO_USED, echoUsed);
        //EventManager.StopListening(EventTypes.SHAPE_SHIFT, shapeShift);
        //EventManager.StopListening(EventTypes.BLOOD_SENT, bloodSentUsed);
    }

    void flyPickedUp() {
        playerResource.addStamina(playerResource.batResourcePickup);
    }

    void echoUsed() {
        playerResource.removeStamina(playerResource.echoCost);
        playerResource.echoUsed();
    }

    //void bloodSentUsed() {
    //    playerResource.humanEaten(playerResource.draculaResourcePickup);
    //}

    //void shapeShift() {
    //    playerControls.ShapeShift();
    //}
}
