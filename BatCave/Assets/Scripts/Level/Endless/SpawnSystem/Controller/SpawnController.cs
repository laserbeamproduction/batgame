using UnityEngine;
using System;

public class SpawnController : MonoBehaviour {
    
	void Start () {
        EventManager.StartListening(SpawnSystemEvents.NEW_BATCH_CREATED, OnBatchNewBatchCreated);
        EventManager.StartListening(SpawnSystemEvents.OBSTACLES_ASSIGNED, OnBatchAssignedObstacles);
        EventManager.StartListening(SpawnSystemEvents.PICKUPS_ASSIGNED, OnBatchAssignedPickups);
        EventManager.StartListening(SpawnSystemEvents.BATCH_READY_FOR_SPAWN, OnBatchDispatched);
    }
    
    void OnDestroy () {
        EventManager.StopListening(SpawnSystemEvents.NEW_BATCH_CREATED, OnBatchNewBatchCreated);
        EventManager.StopListening(SpawnSystemEvents.OBSTACLES_ASSIGNED, OnBatchAssignedObstacles);
        EventManager.StopListening(SpawnSystemEvents.PICKUPS_ASSIGNED, OnBatchAssignedPickups);
        EventManager.StartListening(SpawnSystemEvents.BATCH_READY_FOR_SPAWN, OnBatchDispatched);
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

        // position obstacles and pickups in the spawnpoints in the batch
        foreach (SpawnPointModel spawnPoint in batchModel.GetSpawnPoints()) {
            if (spawnPoint.IsSlotTaken()) {
                if (spawnPoint.GetItem() != null) {
                    spawnPoint.GetItem().gameObject.transform.position = new Vector3(
                        spawnPoint.gameObject.transform.position.x,
                        spawnPoint.gameObject.transform.position.y + UnityEngine.Random.Range(0, batchModel.GetMaxYoffset()),
                        spawnPoint.gameObject.transform.position.z
                        );
                } 
            }
        }

        // pass model back to the view for display
        EventManager.TriggerEvent(SpawnSystemEvents.BATCH_READY_FOR_SPAWN, bm);
    }

    void OnBatchDispatched(object bm) {
        BatchModel batchModel = (BatchModel)bm;
        batchModel.ResetSpawnPoints();
    }
}
