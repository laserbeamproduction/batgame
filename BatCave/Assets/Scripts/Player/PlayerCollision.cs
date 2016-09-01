﻿using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
    public ScoreCalculator score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacle") //simple check if player is colliding with obstacles
        {
            PlayerPrefs.SetFloat("playerScore", score.playerScore); //set score in playerpref
            LoadingController.LoadScene(LoadingController.Scenes.GAME_OVER);
        }
    }
}
