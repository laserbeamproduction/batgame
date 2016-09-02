using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {
    public ScoreCalculator score;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "Score: " + Mathf.FloorToInt(score.playerScore).ToString();
	}
}
