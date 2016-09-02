using UnityEngine;
using System.Collections;

public class ObstaclesSpawner : MonoBehaviour
{

    public float yDistance = 10;
    public float minSpread = 0;
    public float maxSpread = 5;
    private double delay = 0;

    public Transform playerTransform;

    public Transform[] obstacles;
    //float ySpread;
    //float lastYPos;

    void Start()
    {
        //lastYPos = Mathf.NegativeInfinity;
        //ySpread = Random.Range(minSpread, maxSpread);
        //spawnpool = new Array();
    }

    void Update()
    {
        delay -= 0.01;

        if (canSpawn())
        {
            float lanePos = Random.Range(0f, 3f);
            lanePos = (lanePos - 1) * 1.5f;
            Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(lanePos, playerTransform.position.y + yDistance, 0), Quaternion.identity);

            //lastYPos = playerTransform.position.y;
            //ySpread = Random.Range(minSpread, maxSpread);

            delay = Random.Range(0.5f, 2f);
        }
    }

    bool canSpawn() {
        if (delay <= 0) {
            return true;
        } else {
            return false;
        }
    }
}
