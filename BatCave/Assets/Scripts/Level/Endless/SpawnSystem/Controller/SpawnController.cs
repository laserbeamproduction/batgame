using UnityEngine;
using System;

public class SpawnController : MonoBehaviour {

    private bool tensionActive;

	void Start () {
        EventManager.StartListening(SpawnSystemEvents.NEW_BATCH_CREATED, OnBatchNewBatchCreated);
        EventManager.StartListening(SpawnSystemEvents.OBSTACLES_ASSIGNED, OnBatchAssignedObstacles);
        EventManager.StartListening(SpawnSystemEvents.PICKUPS_ASSIGNED, OnBatchAssignedPickups);
        EventManager.StartListening(SpawnSystemEvents.BATCH_READY_FOR_SPAWN, OnBatchDispatched);
        EventManager.StartListening(SpawnSystemEvents.UNLOCK_TENSION_MOMENTS, OnTensionMomentsUnlocked);
        EventManager.StartListening(SpawnSystemEvents.START_TENSION_MOMENT, OnTensionStarted);
        EventManager.StartListening(SpawnSystemEvents.STOP_TENSION_MOMENT, OnTensionStopped);
    }

    void OnDestroy () {
        EventManager.StopListening(SpawnSystemEvents.NEW_BATCH_CREATED, OnBatchNewBatchCreated);
        EventManager.StopListening(SpawnSystemEvents.OBSTACLES_ASSIGNED, OnBatchAssignedObstacles);
        EventManager.StopListening(SpawnSystemEvents.PICKUPS_ASSIGNED, OnBatchAssignedPickups);
        EventManager.StopListening(SpawnSystemEvents.BATCH_READY_FOR_SPAWN, OnBatchDispatched);
        EventManager.StopListening(SpawnSystemEvents.UNLOCK_TENSION_MOMENTS, OnTensionMomentsUnlocked);
        EventManager.StopListening(SpawnSystemEvents.START_TENSION_MOMENT, OnTensionStarted);
        EventManager.StopListening(SpawnSystemEvents.STOP_TENSION_MOMENT, OnTensionStopped);
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
    
    private void OnTensionMomentsUnlocked(object arg0) {
        throw new NotImplementedException();
    }

    private void OnTensionStarted(object arg0) {
        tensionActive = true;
    }

    private void OnTensionStopped(object arg0) {
        tensionActive = false;
    }

    void OnBatchAssignedPickups(object bm) {
        BatchModel batchModel = (BatchModel) bm;

        float yOffset = tensionActive ? 0 : UnityEngine.Random.Range(0, batchModel.GetMaxYoffset());

        // position obstacles and pickups in the spawnpoints in the batch
        foreach (SpawnPointModel spawnPoint in batchModel.GetSpawnPoints()) {
            if (spawnPoint.IsSlotTaken()) {
                if (spawnPoint.GetItem() != null) {
                    spawnPoint.GetItem().gameObject.transform.position = new Vector3(
                        spawnPoint.gameObject.transform.position.x,
                        spawnPoint.gameObject.transform.position.y + yOffset,
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
