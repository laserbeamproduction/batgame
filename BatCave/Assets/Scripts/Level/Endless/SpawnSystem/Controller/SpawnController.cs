using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {
    
	void Start () {
        EventManager.StartListening(SpawnSystemEvents.NEW_BATCH_CREATED, OnBatchNewBatchCreated);
        EventManager.StartListening(SpawnSystemEvents.OBSTACLES_ASSIGNED, OnBatchAssignedObstacles);
        EventManager.StartListening(SpawnSystemEvents.PICKUPS_ASSIGNED, OnBatchAssignedPickups);
    }
    
    void OnDestroy () {
        EventManager.StopListening(SpawnSystemEvents.NEW_BATCH_CREATED, OnBatchNewBatchCreated);
        EventManager.StopListening(SpawnSystemEvents.OBSTACLES_ASSIGNED, OnBatchAssignedObstacles);
        EventManager.StopListening(SpawnSystemEvents.PICKUPS_ASSIGNED, OnBatchAssignedPickups);
    }

    void OnBatchNewBatchCreated(object bm) {
        BatchModel batchModel = (BatchModel) bm;

        // first give batch obstacles by passing the model to the obstacle controller
        EventManager.TriggerEvent(SpawnSystemEvents.ASSIGN_OBSTACLES_TO_BATCH, bm);
    }

    void OnBatchAssignedObstacles(object bm) {
        BatchModel batchModel = (BatchModel) bm;

        // after the batch has obstacles -> pass it to the pickup controller
        EventManager.TriggerEvent(SpawnSystemEvents.ASSIGN_PICKUPS_TO_BATCH, bm);
    }

    void OnBatchAssignedPickups(object bm) {
        BatchModel batchModel = (BatchModel) bm;
        // position obstacles and pickups in the batch
        // todo: ...

        // pass model back to the view for display
        EventManager.TriggerEvent(SpawnSystemEvents.BATCH_READY_FOR_SPAWN, bm);
    }
}
