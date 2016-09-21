using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkInstantiator : MonoBehaviour
{
    //Obstacles & Pickups & Powerups
    public GameObject[] obstacles;
    public GameObject[] pickUps;
    public GameObject player;

    //Instantiated objects
    public static List<GameObject> netObstacles;
    public static List<GameObject> netPickups;

    public Transform spawnPoint;

    void Start() {
        netObstacles = new List<GameObject>();
        netPickups = new List<GameObject>();
    }

    public void InstantiateNetworkObjectPool() {
    //Instantiate all objects on the network for shared pool
        for (int i = 0; i < obstacles.Length;  i++)
        {
            netObstacles.Add(Network.Instantiate(obstacles[i], spawnPoint.position, Quaternion.identity, 0) as GameObject);
        }

        for (int i = 0; i < pickUps.Length; i++)
        {
            netPickups.Add(Network.Instantiate(pickUps[i], spawnPoint.position, Quaternion.identity, 0) as GameObject);
        }
    }    

    //Instantiate players
    public void InstantiatePlayerOne() {
        Network.Instantiate(player, new Vector3(-1, -3, 0), Quaternion.identity, 1);
    }

    public void InstantiatePlayerTwo() {
        Network.Instantiate(player, new Vector3(1, -3, 0), Quaternion.identity, 1);
    }
}
