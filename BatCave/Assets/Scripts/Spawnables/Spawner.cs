using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Spawner : MonoBehaviour {
    public float pickUpYDistance = 20;
    public float obstacleYDistance = 15;

    private double obstacleDelay = 0;
    private double pickUpDelay = 0;

    public float minPickUpDelay = 0;
    public float maxPickUpDelay = 0;

    public float minObstacleDelay = 0;
    public float maxObstacleDelay = 2;

    public int maxActiveObstacles = 5;
    public int maxActivePickups = 3;

    public Transform playerTransform;
    public GameObject[] pickUps;
    public GameObject[] obstacles;
    public Transform[] spawnPoints;

    private bool CanStartSpawning = false;

    void Start() {
        EventManager.StartListening(EventTypes.PLAYER_FLY_IN, StartSpawning);

        foreach (GameObject obj in obstacles) {
            obj.SetActive(false);
        }

        foreach (GameObject obj in pickUps) {
            obj.SetActive(false);
        }
    }

    void FixedUpdate() {
        if (CanStartSpawning)
        {
            spawnObjects();
        }

        pickUpDelay -= 0.01;
        obstacleDelay -= 0.01;
    }

    void StartSpawning() {
        CanStartSpawning = true;
    }

    void spawnObjects() {
        int amountOfObstaclesToSpawn = Random.Range(3, maxActiveObstacles);
        int amountOfPickUpsToSpawn = Random.Range(2, maxActivePickups);
        int currentObstacles = 0;
        int currentPickups = 0;

        if (canSpawnObstacle()) {
            for (int i = 0; i < amountOfObstaclesToSpawn; i++) {
                foreach (GameObject obstacle in obstacles) {
                    if (!obstacle.activeInHierarchy && currentObstacles < amountOfObstaclesToSpawn) {
                        
                        int randomYOffSet = Random.Range(0, 16); // TODO: Set this position to a random lane

                        obstacle.SetActive(true);
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
                    if (!pickup.activeInHierarchy && currentPickups < amountOfPickUpsToSpawn) {

                        int randomYOffSet = Random.Range(0, 8); // TODO: Set this position to a random lane

                        pickup.SetActive(true);
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
}
