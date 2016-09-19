using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {

    void Start() {
        EventManager.StartListening(SpawnSystemEvents.ASSIGN_PICKUPS_TO_BATCH, OnBatchRecieved);
    }

    void OnDestroy() {
        EventManager.StopListening(SpawnSystemEvents.ASSIGN_PICKUPS_TO_BATCH, OnBatchRecieved);
    }

    void OnBatchRecieved(object bm) {
        BatchModel batchModel = (BatchModel)bm;
        // todo: .. ask every pickup that is available in the batch model if it should spawn
        // Stop asking when all spawnpoints are taken or if all available pickups have been asked

        // dispatch ready
        EventManager.TriggerEvent(SpawnSystemEvents.OBSTACLES_ASSIGNED, batchModel);
    }
}
