using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

    void Start() {
        EventManager.StartListening(SpawnSystemEvents.ASSIGN_OBSTACLES_TO_BATCH, OnBatchRecieved);
    }

    void OnDestroy() {
        EventManager.StopListening(SpawnSystemEvents.ASSIGN_OBSTACLES_TO_BATCH, OnBatchRecieved);
    }

    void OnBatchRecieved(object bm) {
        BatchModel batchModel = (BatchModel) bm;
        // todo: .. pick random obstacles for batch given an amount of resources to spend
        // e.g.: a rock costs 1 resource. a big rock (double lane) costs 2 resources
        // given an amount of 3 resources to spend, this controller should pick available 
        // obstacles from the pool spending no more than the 3 resourcepoints.

        // dispatch ready
        EventManager.TriggerEvent(SpawnSystemEvents.OBSTACLES_ASSIGNED, batchModel);
    }
}
