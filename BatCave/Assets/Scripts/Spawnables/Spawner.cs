using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    void Start()
    {
        for (int i = 0; i < obstacles.Length; i++) {
            obstacles[i].SetActive(false);
        }

        for (int i = 0; i < pickUps.Length; i++)
        {
            pickUps[i].SetActive(false);
        }
    }

    void Update()
    {
        pickUpDelay -= 0.01;
        obstacleDelay -= 0.01;
    }

    void FixedUpdate() {
        spawnObjects();
    }

    void spawnObjects() {
        int obstaclesToSpawn = Random.Range(3, maxActiveObstacles);
        int pickUpsToSpawn = Random.Range(1, maxActivePickups);
        int currentObstacles = 0;
        int currentPickups = 0;

        if (canSpawnObstacle())
        {
            for (int i = 0; i < obstaclesToSpawn; i++)
            {
                for (int j = 0; j < obstacles.Length; j++) {
                    if (obstacles[j].activeInHierarchy == false && currentObstacles < obstaclesToSpawn)
                    {
                        int randomYOffSet = Random.Range(0, 8);
                        GameObject thingToSpawn;
                        thingToSpawn = obstacles[j];
                        thingToSpawn.SetActive(true);
                        thingToSpawn.transform.position = new Vector2(spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position.x, playerTransform.position.y + obstacleYDistance + randomYOffSet);
                        currentObstacles++;
                    }
                }                
            }
            obstacleDelay = Random.Range(minObstacleDelay, maxObstacleDelay);
        }

        if (canSpawnPickUp())
        {
            for (int i = 0; i < pickUpsToSpawn; i++)
            {
                for (int j = 0; j < obstacles.Length; j++)
                {
                    if (obstacles[j].activeInHierarchy == false && currentPickups < pickUpsToSpawn)
                    {
                        int randomYOffSet = Random.Range(0, 8);
                        GameObject thingToSpawn;
                        thingToSpawn = pickUps[j];
                        thingToSpawn.SetActive(true);
                        thingToSpawn.transform.position = new Vector2(spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position.x, playerTransform.position.y + pickUpYDistance + randomYOffSet);
                        currentPickups++;
                    }
                }
            }
            pickUpDelay = Random.Range(minPickUpDelay, maxPickUpDelay);
        }
    }

    bool canSpawnObstacle()
    {
        if (obstacleDelay <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool canSpawnPickUp()
    {
        if (pickUpDelay <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
