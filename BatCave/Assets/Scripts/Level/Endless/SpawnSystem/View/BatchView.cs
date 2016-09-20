using UnityEngine;
using System.Collections;
using System;

public class BatchView : MonoBehaviour {

    public SpawnPointModel[] spawnPoints;
    public PickupModel[] pickups;
    public ObstacleModel[] obstacles;
    public GameObject cleanUp;

    public float spawnDelay;
    public int totalObstacleResources;
    public int totalPickupResouces;
    public float maxYoffset;

    void Start() {
        EventManager.StartListening(SpawnSystemEvents.TOGGLE_SPAWNING, OnSpawningToggled);
        EventManager.StartListening(SpawnSystemEvents.BATCH_READY_FOR_SPAWN, OnBatchReadyForSpawn);
    }

    void OnDestroy() {
        EventManager.StopListening(SpawnSystemEvents.TOGGLE_SPAWNING, OnSpawningToggled);
        EventManager.StopListening(SpawnSystemEvents.BATCH_READY_FOR_SPAWN, OnBatchReadyForSpawn);

    }
    
    private void OnSpawningToggled(object arg0) {
        bool enabled = (bool)arg0;
        cleanUp.SetActive(enabled);
        if (enabled)
            StartCoroutine("CreateNewBatch");
        else
            StopCoroutine("CreateNewBatch");
    }

    IEnumerator CreateNewBatch() {
        while (true) {
            // Create new batch model
            BatchModel batchModel = new BatchModel(spawnPoints, pickups, obstacles, totalObstacleResources, totalPickupResouces, maxYoffset);

            EventManager.TriggerEvent(SpawnSystemEvents.NEW_BATCH_CREATED, batchModel);

            ModSpawnValues();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void ModSpawnValues() {
        // tweak spawn values for the next spawn batch (increasing game difficulty per batch spawned)
        // todo: ...
    }

    private void OnBatchReadyForSpawn(object arg0) {
        // todo read objects in batch -> enable objects 
    }   
}
