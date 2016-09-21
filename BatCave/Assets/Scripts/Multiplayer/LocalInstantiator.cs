using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocalInstantiator : MonoBehaviour {
    //PREFABS
    public GameObject[] playerEchos;
    public GameObject playerDayLight;

    //WHERE TO INITIAL SPAWN
    public Transform spawnPoint;

    //USE IN OTHER SCRIPTS
    public static List<GameObject> localPlayerEchos;
    GameObject localPlayerLight;

    void Start() {
        localPlayerEchos = new List<GameObject>();
    }

    public void InstantiateLocalObjectPool() {
        for (int i = 0; i < playerEchos.Length; i++) {
            GameObject playerEcho = Instantiate(playerEchos[i], spawnPoint.position, Quaternion.identity) as GameObject;
            playerEcho.name = playerEcho.name + i;
            localPlayerEchos.Add(playerEcho as GameObject);
        }

        localPlayerLight = Instantiate(playerDayLight, new Vector3(0,0,0), Quaternion.identity) as GameObject;
    }

}
