using UnityEngine;
using System.Collections;

public class ScoreCalculator : MonoBehaviour {
    private float timePlayed = 0;
    public float scoreMultiplier;
    public float playerScore;
    

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetFloat("playerScore", 0);
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

    void FixedUpdate() {
        playerScore += 1;
    }
}
