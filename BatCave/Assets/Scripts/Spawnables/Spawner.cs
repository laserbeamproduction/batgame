using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public List<GameObject> activeObjects = new List<GameObject>();

    public float pickUpYDistance = 20;
    public float obstacleYDistance = 15;

    private double obstacleDelay = 0;
    private double pickUpDelay = 0;

    public float minPickUpDelay = 0;
    public float maxPickUpDelay = 0;

    public float minObstacleDelay = 0;
    public float maxObstacleDelay = 0;

    public Transform playerTransform;
    public Transform[] pickUps;
    public Transform[] obstacles;
    public Transform[] spawnPoints;

    void Start()
    {

    }

    void Update()
    {
        pickUpDelay -= 0.01;
        obstacleDelay -= 0.01;

        if (canSpawnPickUp())
        {
            activeObjects.Add(Instantiate(pickUps[Random.Range(0, pickUps.Length)], new Vector3(spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position.x, playerTransform.position.y + pickUpYDistance, 0), Quaternion.identity) as GameObject);
            pickUpDelay = Random.Range(minPickUpDelay, maxPickUpDelay);
        }

        if (canSpawnObstacle())
        {
            activeObjects.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position.x, playerTransform.position.y + obstacleYDistance, 0), Quaternion.identity) as GameObject);
            obstacleDelay = Random.Range(minObstacleDelay, maxObstacleDelay);
        }

        activeObjects.RemoveAll(item => item == null);
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
