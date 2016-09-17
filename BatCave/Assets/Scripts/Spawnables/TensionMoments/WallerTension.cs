using UnityEngine;
using System.Collections;

public class WallerTension : TensionController {
    public float delayBetweenWalls;
    public int amountOfObstaclesInWall;
    public int amountOfOpenings;
    public int amountOfWalls;
    public float RestTime;
    public Transform playerTransform;
    public ScoreCalculator scoreCalcultor;
    public int amountOfOpeningsHard;
    public int amountOfWallsHard;
    public int amountOfObstaclesWallHard;
    public float delayBetweenWallsHard;

    private float wallDelayCooldown;
    private bool canStartSpawning;
    private int currentAmountOfWalls;

    void FixedUpdate() {
        if (canStartSpawning) {
            SpawnWalls();
            wallDelayCooldown -= 0.01f;
        }
    }

    void SpawnWalls() {
        int currentRocksInWall = 0;
        int currentOpenings = 0;

        if (currentAmountOfWalls < amountOfWalls)
        {
            if (wallDelayCooldown < 0)
            {
                for (int i = 0; i < base.spawnpoints.Length; i++)
                {
                    if (currentOpenings < amountOfOpenings && currentRocksInWall < amountOfObstaclesInWall)
                    {
                        if (Random.Range(1, 101) < 50)
                        {
                            SpawnWallPeace(i);
                            currentRocksInWall++;
                        }
                        else
                        {
                            if (Random.Range(1, 101) < 10) { SpawnPickUp(i); }
                            currentOpenings++;
                        }
                    }
                    else if (currentOpenings < amountOfOpenings)
                    {
                        if (Random.Range(1, 101) < 10) { SpawnPickUp(i); }
                        currentOpenings++;
                    }
                    else {
                        SpawnWallPeace(i);
                        currentRocksInWall++;
                    }
                }
                currentAmountOfWalls++;
                Debug.Log(currentAmountOfWalls);
                wallDelayCooldown = delayBetweenWalls;
            }
        }
        else {
            StartCoroutine(CoolDownTime());
            canStartSpawning = false;
        }
    }

    void SpawnWallPeace(int index) {
        foreach (GameObject obstacle in base.obstacles) {
            if (!obstacle.GetComponent<SpriteRenderer>().enabled)
            {
                obstacle.GetComponent<SpriteRenderer>().enabled = true;
                obstacle.GetComponent<BoxCollider2D>().enabled = true;
                obstacle.transform.position = new Vector2(base.spawnpoints[index].transform.position.x, playerTransform.position.y + 25);
                return;
            }
        }
    }

    void SpawnPickUp(int index) {
        foreach (GameObject pickup in base.pickups)
        {
            if (!pickup.GetComponent<SpriteRenderer>().enabled)
            {
                pickup.GetComponent<SpriteRenderer>().enabled = true;
                pickup.GetComponent<BoxCollider2D>().enabled = true;
                pickup.transform.position = new Vector2(base.spawnpoints[index].transform.position.x, playerTransform.position.y + 25);
                return;
            }
        }
    }

    public void CanStartSpawning() {
        SetDifficulty();
        canStartSpawning = true;
        currentAmountOfWalls = 0;
    }

    IEnumerator CoolDownTime() {
        yield return new WaitForSeconds(RestTime);
        EventManager.TriggerEvent(EventTypes.STOP_TENSION);
        StopCoroutine(CoolDownTime());
    }

    void SetDifficulty() {
        if (scoreCalcultor.playerScore > 5500) {
            delayBetweenWalls = delayBetweenWallsHard;
            amountOfObstaclesInWall = amountOfObstaclesWallHard;
            amountOfOpenings = amountOfOpeningsHard;
            amountOfWalls = amountOfWallsHard;
        }
    }
}
