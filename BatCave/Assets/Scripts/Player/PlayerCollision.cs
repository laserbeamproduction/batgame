using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
            //Application.LoadLevel("gameOver_scene"); //load gameover menu
            SceneManager.LoadScene("gameOver_scene");
        }
    }
}
