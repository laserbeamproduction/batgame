using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BatchView : MonoBehaviour {

    public bool isNetwork;

    public SpawnPointModel[] spawnPoints;

    public PickupModel[] pickups;
    private PickupModel[] pickupsForStage;

    public ObstacleModel[] obstacles;
    private ObstacleModel[] obstaclesForStage;

    public GameObject cleanUp;
    public ScoreCalculator scoreCalculator;

    public float increaseDifficultyStep;
    public float highestDifficultyScore;
    public float stageDurationInScore;

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

    private int currentStage = 0;

    void Start() {
        EventManager.StartListening(EventTypes.START_COUNTDOWN, OnServerStarted);
        EventManager.StartListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StartListening(EventTypes.GAME_RESUME, OnGameResumed);
        EventManager.StartListening(EventTypes.CHANGE_ENVIRONMENT, OnEnviromentChanged);
        if (!isNetwork) 
            EventManager.StartListening(SpawnSystemEvents.TOGGLE_SPAWNING, OnSpawningToggled);
    }

    void OnEnviromentChanged(object type) {
        EventManager.TriggerEvent(SpawnSystemEvents.TOGGLE_SPAWNING, true);
        currentStage++;

        // select only available pickups and obstacles
        List<PickupModel> pickupList = new List<PickupModel>();
        foreach (PickupModel pm in pickups) {
            if (pm.GetStageLevel() >= currentStage) {
                pickupList.Add(pm);
            }
        }

        List<ObstacleModel> obstacleList = new List<ObstacleModel>();
        foreach (ObstacleModel om in obstacles) {
            if (om.GetStageLevel() >= currentStage) {
                obstacleList.Add(om);
            }
        }

        pickupsForStage = pickupList.ToArray();
        obstaclesForStage = obstacleList.ToArray();
    }

    void OnGameResumed(object arg0) {
        OnSpawningToggled(true);
    }

    void OnGamePaused(object arg0) {
        OnSpawningToggled(false);
    }

    void OnDestroy() {
        EventManager.StopListening(SpawnSystemEvents.TOGGLE_SPAWNING, OnSpawningToggled);
        EventManager.StopListening(EventTypes.START_COUNTDOWN, OnServerStarted);
        EventManager.StopListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StopListening(EventTypes.GAME_RESUME, OnGameResumed);
        EventManager.StopListening(EventTypes.CHANGE_ENVIRONMENT, OnEnviromentChanged);
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
            }

            if (score % stageDurationInScore == 0 && score != 0 ) {
                EventManager.TriggerEvent(EventTypes.TRANSITION_START);
                EventManager.TriggerEvent(SpawnSystemEvents.TOGGLE_SPAWNING, false);
            }

            if (score == 100) {
                EventManager.TriggerEvent(EventTypes.CHANGE_ENVIRONMENT);
            }
        }
    }
    
    private void OnSpawningToggled(object arg0) {
        bool enabled = (bool)arg0;
        if (!cleanUp.activeInHierarchy)
            cleanUp.SetActive(true);
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
