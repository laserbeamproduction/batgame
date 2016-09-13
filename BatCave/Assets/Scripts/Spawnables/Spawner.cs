using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Spawner : MonoBehaviour {
    private float pickUpYDistance = 16;
    private float obstacleYDistance = 12;

    private double obstacleDelay = 0;
    private double pickUpDelay = 0;

    public float minPickUpDelay = 0;
    public float maxPickUpDelay = 0;

    public float minObstacleDelay = 0;
    public float maxObstacleDelay = 2;

    //Needed for difficulty curve
    public int minActiveObstacles = 1;
    public int minActivePickups = 1;
    public int maxActiveObstacles = 4;
    public int maxActivePickups = 3;

    //Keep track of current data
    private int currentActiveObstacles;
    private int currentActivePickups;

    //How long is the difficulty curve
    public float difficultyIntervalTime;

    public Transform playerTransform;
    public GameObject[] pickUps;
    public GameObject[] obstacles;
    public Transform[] spawnPoints;
    public GameObject cleanUp;

    private bool CanStartSpawning = false;

    //Needed for difficulty curve


    private bool increaseSpawnAmount = true;

    void Start() {
        EventManager.StartListening(EventTypes.PLAYER_IN_POSITION, StartSpawning);

        currentActiveObstacles = minActiveObstacles;
        currentActivePickups = minActivePickups;
        StartCoroutine(StartTimer());
    }

    void FixedUpdate() {
        if (CanStartSpawning)
        {
            DifficultyCurve();
            spawnObjects();
        }

        pickUpDelay -= 0.01;
        obstacleDelay -= 0.01;
    }

    void StartSpawning() {
        CanStartSpawning = true;
        cleanUp.SetActive(true);
    }

    void DifficultyCurve() {

    }

    void spawnObjects() {
        int amountOfObstaclesToSpawn = Random.Range(minActiveObstacles, currentActiveObstacles);
        int amountOfPickUpsToSpawn = Random.Range(minActivePickups, currentActivePickups);
        int currentObstacles = 0;
        int currentPickups = 0;

        if (canSpawnObstacle()) {
            for (int i = 0; i < amountOfObstaclesToSpawn; i++) {
                foreach (GameObject obstacle in obstacles) {
                    if (!obstacle.GetComponent<SpriteRenderer>().enabled && currentObstacles < amountOfObstaclesToSpawn) {
                        
                        int randomYOffSet = Random.Range(0, 16); // TODO: Set this position to a random lane
                        obstacle.GetComponent<SpriteRenderer>().enabled = true;
                        obstacle.transform.position = new Vector2(spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position.x, playerTransform.position.y + obstacleYDistance + randomYOffSet);
                        currentObstacles++;
                    }
                }                
            }
            obstacleDelay = Random.Range(minObstacleDelay, maxObstacleDelay);
        }

        if (canSpawnPickUp()) {
            for (int i = 0; i < amountOfPickUpsToSpawn; i++) {
                foreach (GameObject pickup in pickUps) {
                    if (!pickup.GetComponent<SpriteRenderer>().enabled && currentPickups < amountOfPickUpsToSpawn) {

                        int randomYOffSet = Random.Range(0, 8); // TODO: Set this position to a random lane

                        //pickup.SetActive(true);
                        pickup.GetComponent<SpriteRenderer>().enabled = true;
                        pickup.GetComponent<BoxCollider2D>().enabled = true;
                        pickup.transform.position = new Vector2(spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position.x, playerTransform.position.y + pickUpYDistance + randomYOffSet);
                        currentPickups++;
                    }
                }
            }
            pickUpDelay = Random.Range(minPickUpDelay, maxPickUpDelay);
        }
    }

    bool canSpawnObstacle() {
        return obstacleDelay <= 0;
    }

    bool canSpawnPickUp() {
        return pickUpDelay <= 0;
    }

    IEnumerator StartTimer()
    {
        while (increaseSpawnAmount)
        {
            yield return new WaitForSeconds(difficultyIntervalTime);

            if (currentActiveObstacles < maxActiveObstacles) {
                currentActiveObstacles++;
            }

            if (currentActivePickups < maxActivePickups) {
                currentActivePickups++;
            }

            if (currentActiveObstacles >= maxActiveObstacles && currentActivePickups >= maxActivePickups) {
                increaseSpawnAmount = false;
            }
        }
    }
}
