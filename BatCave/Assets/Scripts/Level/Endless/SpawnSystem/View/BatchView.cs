using UnityEngine;
using System.Collections;

public class BatchView : MonoBehaviour {

    public SpawnPointModel[] spawnPoints;
    public PickupModel[] pickups;
    public ObstacleModel[] obstacles;

    public float spawnDelay;
    public int totalObstacleResources;
    public int totalPickupResouces;
    public float maxYoffset;
    
	// Use this for initialization
	void Start () {
        StartCoroutine("CreateNewBatch");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator CreateNewBatch() {
        yield return new WaitForSeconds(spawnDelay);

        // Create new batch model
        BatchModel batchModel = new BatchModel(spawnPoints, pickups, obstacles);

        // Dispatch assign items to slots in batch model
        // ...

    }

    void OnBatchSpawnReady(object batch) {
        // todo read objects in batch -> position at spawnpoints -> reset countdown
    }
}
