using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public List<Transform> activeObjects;

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
        activeObjects = new List<Transform>();
    }

    void Update()
    {
        pickUpDelay -= 0.01;
        obstacleDelay -= 0.01;

        if (canSpawnPickUp())
        {
            activeObjects.Add(Instantiate(pickUps[Random.Range(0, pickUps.Length)], new Vector3(spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position.x, playerTransform.position.y + pickUpYDistance, 0), Quaternion.identity) as Transform);
            pickUpDelay = Random.Range(minPickUpDelay, maxPickUpDelay);
        }

        if (canSpawnObstacle())
        {
            activeObjects.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position.x, playerTransform.position.y + obstacleYDistance, 0), Quaternion.identity) as Transform);
            obstacleDelay = Random.Range(minObstacleDelay, maxObstacleDelay);
        }

        
        activeObjects.RemoveAll(item => item == null);
    }

    void FixedUpdate() {
        checkMarkedForDestroyed(); //no need to update every frame.
    }

    void checkMarkedForDestroyed() {
        int i = 0;
        foreach (Transform obj in activeObjects)
        {
            if (activeObjects[i].gameObject.tag == "fly") {
                if (activeObjects[i].GetComponent<FlyController>().markedForDestroy == true) {
                    Destroy(activeObjects[i].gameObject);
                }
            }

            if (activeObjects[i].gameObject.tag == "Obstacle") {
                if (activeObjects[i].GetComponent<ObstacleCleanUp>().markedForDestroy == true)
                {
                    Destroy(activeObjects[i].gameObject);
                }
            }

            i++;
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
