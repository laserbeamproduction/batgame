using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {
    public ScoreCalculator score;

    private Text text;
    private bool isPaused;
    private bool isIntro = true;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        EventManager.StartListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StartListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StartListening(EventTypes.PLAYER_IN_POSITION, OnIntroCompleet);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPaused && !isIntro)
            text.text = Mathf.FloorToInt(score.playerScore).ToString();
	}

    void OnDestroy() {
        EventManager.StopListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StopListening(EventTypes.GAME_PAUSED, OnGamePaused);
    }

    void OnGamePaused() {
        isPaused = true;
    }

    void OnGameResume() {
        isPaused = false;
    }

    void OnIntroCompleet() {
        isIntro = false;
    }
}
