using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BatchView : MonoBehaviour {

    public bool isNetwork;

    public SpawnPointModel[] spawnPoints;
    public PickupModel[] pickups;
    public ObstacleModel[] obstacles;
    public GameObject cleanUp;
    public ScoreCalculator scoreCalculator;

    public float increaseDifficultyStep;
    public float highestDifficultyScore;
    public float tensionMomentStep;
    public float startSpawningPickupsScore;

    public float initialSpawnDelay;
    public float endSpawnDelay;
    public float currentSpawnDelay;

    public int initialTotalObstacleResources;
    public int endTotalObstacleResources;
    public int currentTotalObstacleResources;

    public int initialTotalPickupResouces;
    public int endTotalPickupResouces;
    public int currentTotalPickupResources;

    public float initialYoffsetError;
    public float endYoffsetError;
    public float currentYoffsetError;

    private bool powerupsEnabled;


    void Start() {
        EventManager.StartListening(EventTypes.SERVER_STARTED, OnServerStarted);
        if (!isNetwork) 
            EventManager.StartListening(SpawnSystemEvents.TOGGLE_SPAWNING, OnSpawningToggled);
        
    }

    void OnDestroy() {
        EventManager.StopListening(SpawnSystemEvents.TOGGLE_SPAWNING, OnSpawningToggled);
        EventManager.StopListening(EventTypes.SERVER_STARTED, OnServerStarted);

    }

    float UpdateCurrentValueByScore(float currentDifficultyStep, float totalDifficultySteps , float initValue, float endValue) {
        float highest = Math.Max(initValue, endValue); 
        float lowest = Math.Min(initValue, endValue); 
        float a = (highest - lowest) / totalDifficultySteps; 
        if (endValue > initValue) {
            return initValue + (a * currentDifficultyStep);
        } else {
            return initValue - (a * currentDifficultyStep);
        }
    }

    void FixedUpdate() {
        // tweak spawn stats depending on current score
        if (!isNetwork) {
            float score = scoreCalculator.playerScore;
            if (score <= highestDifficultyScore && score != 0) {

                // check if difficulty needs to go up
                if (score % increaseDifficultyStep == 0) {
                    // increase difficulty
                    float currentStep = score / increaseDifficultyStep;
                    float totalDifficultySteps = highestDifficultyScore / increaseDifficultyStep;

                    currentSpawnDelay = UpdateCurrentValueByScore(currentStep, totalDifficultySteps, initialSpawnDelay, endSpawnDelay);
                    currentTotalObstacleResources = (int)UpdateCurrentValueByScore(currentStep, totalDifficultySteps, initialTotalObstacleResources, endTotalObstacleResources);
                    currentTotalPickupResources = (int)UpdateCurrentValueByScore(currentStep, totalDifficultySteps, initialTotalPickupResouces, endTotalPickupResouces);
                    currentYoffsetError = UpdateCurrentValueByScore(currentStep, totalDifficultySteps, initialYoffsetError, endYoffsetError);
                }

                // check if we start a tension moment
                if (score % tensionMomentStep == 0) {
                    // dispatch start tension
                }

                // check if pick ups need to be enabled
                if (!powerupsEnabled && score > startSpawningPickupsScore) {
                    // dispatch power ups enabled
                }
            }
        }
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
            BatchModel batchModel = new BatchModel(spawnPoints, pickups, obstacles, currentTotalObstacleResources, currentTotalPickupResources, currentYoffsetError);

            EventManager.TriggerEvent(SpawnSystemEvents.NEW_BATCH_CREATED, batchModel);
            
            yield return new WaitForSeconds(currentSpawnDelay);
        }
    }

    private void OnServerStarted(object arg0) {
        if (!Network.isClient && Network.isServer) {
            EventManager.StartListening(SpawnSystemEvents.TOGGLE_SPAWNING, OnSpawningToggled);
            
            List<PickupModel> netPickups = new List<PickupModel>();
            List<ObstacleModel> netObstacles = new List<ObstacleModel>();

            for (int i = 0; i < obstacles.Length; i++) {
                netObstacles.Add(Network.Instantiate(obstacles[i], new Vector3(-30f, 0f, 0f), Quaternion.identity, 0) as ObstacleModel);
            }

            for (int i = 0; i < pickups.Length; i++) {
                netPickups.Add(Network.Instantiate(pickups[i], new Vector3(-30f, 0f, 0f), Quaternion.identity, 0) as PickupModel);
            }

            obstacles = netObstacles.ToArray();
            pickups = netPickups.ToArray();
        }
    }
}
