using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
    public GameObject pausePanel;
    public Light directionalLight;
    public float fadeOutDelay;

    public float playerDiesTime;

    private float playerDiesCounter;
    private float fadeOutDelayCounter;
    private bool playerDied;

	// Use this for initialization
	void Start () {
        EventManager.StartListening(EventTypes.GAME_OVER, OnGameOver);
        EventManager.StartListening(EventTypes.GAME_START, OnGameStart);
        EventManager.StartListening(EventTypes.PLAYER_DIED, OnPlayerDied);

        EventManager.TriggerEvent(EventTypes.GAME_START);
    }

    void FixedUpdate() {
        if (directionalLight != null) {
            if (fadeOutDelayCounter >= fadeOutDelay) {
                directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 0f, 1f * Time.deltaTime);
                if (directionalLight.intensity <= 0.5f) {
                    Destroy(directionalLight.gameObject);
                    EventManager.TriggerEvent(EventTypes.PLAYER_FLY_IN);
                }
            } else {
                fadeOutDelayCounter += Time.deltaTime;
            }
        }
    }

    void Update() {
        if (playerDied) {
            playerDiesCounter += Time.deltaTime;
            if (playerDiesCounter >= playerDiesTime) {
                playerDied = false;
                EventManager.TriggerEvent(EventTypes.GAME_OVER);
            }
        }
    }

    void OnPlayerDied() {
        playerDied = true;
    }

    void OnGameStart() {
        // reset player model
        SaveLoadController.GetInstance().GetEndlessSession().Reset();
    }

    public void PauseGame() {
        EventManager.TriggerEvent(EventTypes.GAME_PAUSED);
        pausePanel.SetActive(true);
    }

    public void ResumeGame() {
        pausePanel.SetActive(false);
        EventManager.TriggerEvent(EventTypes.GAME_RESUME);
    }

    void OnGameOver() {
        PlayerSave player = SaveLoadController.GetInstance().GetPlayer();
        EndlessSessionSave gameSession = SaveLoadController.GetInstance().GetEndlessSession();
        GooglePlayHelper gph = GooglePlayHelper.GetInstance();
        player.AddTotalGamesPlayed(1);

        // report events
        gph.ReportEvent(GPGSConstant.event_amount_of_endless_games_started, 1);
        gph.ReportEvent(GPGSConstant.event_score_endless_mode, gameSession.GetTotalScore());
        gph.ReportEvent(GPGSConstant.event_amount_of_flies_gathered_endless, gameSession.GetResourcesGathered());

        // save current stats
        gph.SaveGame(); // TODO: keep track of timeplayed

        // check for achievements
        AchievementChecker.CheckForEndlessScoreAchievement(gameSession.GetTotalScore());

        // highscore post
        if (gameSession.GetTotalScore() > player.GetHighscore()) {
            EventManager.TriggerEvent(EventTypes.NEW_HIGHSCORE);
            player.SetHighscore(gameSession.GetTotalScore());
            gph.PostHighscore(player.GetHighscore(), GPGSConstant.leaderboard_endless_mode);
        }
        
        // start game over screen
        LoadingController.LoadScene(LoadingController.Scenes.GAME_OVER);

    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.GAME_OVER, OnGameOver);
        EventManager.StopListening(EventTypes.GAME_START, OnGameStart);
    }
}
