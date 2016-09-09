using UnityEngine;
using System.Collections;

public class ScoreCalculator : MonoBehaviour {
    private float timePlayed = 0;
    public float scoreMultiplier;
    public float playerScore;
    
	void Start () {
        EventManager.StartListening(EventTypes.GAME_OVER, OnGameOver);
    }
	
	// Update is called once per frame
	void Update () {
        if (scoreMultiplier < 0) {
            scoreMultiplier = 1;
        }
       
        if (playerScore < 0) {
            playerScore = 0;
        }
	}

    void OnDestroy() {
        EventManager.StopListening(EventTypes.GAME_OVER, OnGameOver);
    }

    void FixedUpdate() {
        playerScore += 1;
    }

    void OnGameOver() {
        SaveLoadController.GetInstance().GetEndlessSession().SetScore(playerScore);
    }
}
