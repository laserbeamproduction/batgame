using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocalInstantiator : MonoBehaviour {
    //PREFABS
    public GameObject[] playerEchos;
    public GameObject playerDayLight;

    //WHERE TO INITIAL SPAWN
    public Transform echoSpawnpoint;
    public Transform dayLightSpawnpoint;

    //USE IN OTHER SCRIPTS
    public static List<GameObject> localPlayerEchos;
    GameObject localPlayerLight;

    void Start() {
        localPlayerEchos = new List<GameObject>();
    }

    public void InstantiateLocalObjectPool() {
        for (int i = 0; i < playerEchos.Length; i++) {
            GameObject playerEcho = Instantiate(playerEchos[i], echoSpawnpoint.position, Quaternion.identity) as GameObject;
            playerEcho.name = playerEcho.name + i;
            localPlayerEchos.Add(playerEcho as GameObject);
        }

        localPlayerLight = Instantiate(playerDayLight, dayLightSpawnpoint.position, Quaternion.identity) as GameObject;
    }

}
