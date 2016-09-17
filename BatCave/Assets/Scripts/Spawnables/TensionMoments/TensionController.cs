using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

abstract public class TensionController : MonoBehaviour {
    public GameObject[] obstacles;
    public GameObject[] pickups;
    public GameObject[] powerups;
    public Transform[] spawnpoints;

    //add tensions
    public WallerTension wallerTension;

    private void Start() {
        EventManager.StartListening(EventTypes.START_TENSION, PickAndStartTension);
        EventManager.StartListening(EventTypes.STOP_TENSION, TensionStopped);
    }

    private void PickAndStartTension(Dictionary<string, object> arg0) {
        EventManager.TriggerEvent(EventTypes.STOP_SPAWNING, null);
        //TODO:
        //randomly pick tension moment
        wallerTension.CanStartSpawning();
    }

    private void TensionStopped(Dictionary<string, object> arg0) {
        EventManager.TriggerEvent(EventTypes.START_SPAWNING, null);
    }
}
