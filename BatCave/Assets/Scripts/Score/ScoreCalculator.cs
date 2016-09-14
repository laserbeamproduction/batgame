using UnityEngine;
using System.Collections;

public class ScoreCalculator : MonoBehaviour {
    private float timePlayed = 0;
    public float scoreMultiplier;
    public float playerScore;

    private bool isPaused;
    private bool gameStarted;


    void Start () {
        EventManager.StartListening(EventTypes.GAME_OVER, OnGameOver);
        EventManager.StartListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StartListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StartListening(EventTypes.PLAYER_DIED, OnGamePaused);
        EventManager.StartListening(EventTypes.PLAYER_IN_POSITION, OnIntroCompleet);
        EventManager.StartListening(EventTypes.PLAYER_SPEED_PICKUP, SetSpeedMultiplier);
        EventManager.StartListening(EventTypes.PLAYER_SPEED_ENDED, SetSpeedMultiplier);
    }

    void OnGamePaused() {
        isPaused = true;
    }

    void OnGameResume() {
        isPaused = false;
    }

    void OnIntroCompleet() {
        gameStarted = true;
    }

    // Update is called once per frame
    void Update () {
        if (!isPaused) {
            if (scoreMultiplier < 0) {
                scoreMultiplier = 1;
            }

            if (playerScore < 0) {
                playerScore = 0;
            }
        }
	}

    void OnDestroy() {
        EventManager.StopListening(EventTypes.GAME_OVER, OnGameOver);
        EventManager.StopListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StopListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StopListening(EventTypes.PLAYER_DIED, OnGamePaused);
        EventManager.StopListening(EventTypes.PLAYER_IN_POSITION, OnIntroCompleet);
    }

    void FixedUpdate() {
        if (!isPaused && gameStarted) 
            playerScore += 1 * scoreMultiplier;
    }

    void OnGameOver() {
        SaveLoadController.GetInstance().GetEndlessSession().SetScore(playerScore);
    }

    void SetSpeedMultiplier() {
        if (scoreMultiplier == 3) {
            scoreMultiplier = 1;
        } else {
            scoreMultiplier = 3;
        }
    }
}
