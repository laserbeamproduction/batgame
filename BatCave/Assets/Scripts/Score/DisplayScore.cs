using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisplayScore : MonoBehaviour {
    public ScoreCalculator score;

    private Text text;
    public Text coinText;
    private bool isPaused;

    private EndlessSessionSave ess;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        text.text = "0";
        ess = SaveLoadController.GetInstance().GetEndlessSession();
        EventManager.StartListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StartListening(EventTypes.GAME_PAUSED, OnGamePaused);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPaused) {
            text.text = Mathf.FloorToInt(score.playerScore).ToString();
            coinText.text = ess.GetTotalCoinsCollected().ToString();
        }
	}

    void OnDestroy() {
        EventManager.StopListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StopListening(EventTypes.GAME_PAUSED, OnGamePaused);
    }

    void OnGamePaused(object arg0) {
        isPaused = true;
    }

    void OnGameResume(object arg0) {
        isPaused = false;
    }
}
