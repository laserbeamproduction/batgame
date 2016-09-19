using UnityEngine;
using System.Collections;

public class NetworkInstantiator : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject[] pickUps;
    public GameObject[] powerUps;

    public int obstaclePoolSize;
    public int pickUpsPoolSize;
    public int powerUpsPoolSize;

    public Transform spawnPoint;

    private bool canSpawn = false;

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
            SpawnObjectPool();
    }

    void SpawnObjectPool() {
    //Instantiate all objects on the network for shared pool
        for (int i = 0; i < obstaclePoolSize; i++)
        {
            Network.Instantiate(obstacles[i], spawnPoint.position, Quaternion.identity, 0);
        }

        for (int i = 0; i < pickUpsPoolSize; i++)
        {
            Network.Instantiate(pickUps[i], spawnPoint.position, Quaternion.identity, 0);
        }

        for (int i = 0; i < powerUpsPoolSize; i++)
        {
            Network.Instantiate(powerUps[i], spawnPoint.position, Quaternion.identity, 0);
        }

        canSpawn = false;
    }

    public void CanStartSpawning() {
        canSpawn = true;
    }
}
