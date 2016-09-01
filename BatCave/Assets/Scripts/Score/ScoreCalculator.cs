using UnityEngine;
using System.Collections;

public class ScoreCalculator : MonoBehaviour {
    private float timePlayed = 0;
    public float scoreMultiplier;
    public float playerScore;
    public PlayerControls player;
    

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetFloat("playerScore", 0);
    }
	
	// Update is called once per frame
	void Update () {
        timePlayed += Time.deltaTime;
        scoreMultiplier = (timePlayed/2) - (player.echoAmount * 2);

        if (scoreMultiplier < 0) {
            scoreMultiplier = 1;
        }

        playerScore += (timePlayed * 3) * scoreMultiplier;

        if (playerScore < 0) {
            playerScore = 0;
        }
	}
}
