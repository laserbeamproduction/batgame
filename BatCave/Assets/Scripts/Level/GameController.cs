using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject camera;
    public GameObject scorePanel;
    public GameObject skillSlider;
    public ScoreCalculator scoreCalculator;
    public Light directionalLight;
    public float fadeOutDelay;
    public float fadeOutSpeed;
    public float playerFliesInDelay;
    public int scoreIntervalTension;
    public int scoreForPowerups;

    public float playerDiesTime;

    private float playerDiesCounter;
    private float fadeOutDelayCounter;
    private float playerFliesInCounter;
    private bool playerDied;
    private bool playerFlyInTriggered;
    private bool playerInPosition;
    private bool powerUpsActive = false;

    // Use this for initialization
    void Start() {
        EventManager.StartListening(EventTypes.GAME_OVER, OnGameOver);
        EventManager.StartListening(EventTypes.GAME_START, OnGameStart);
        EventManager.StartListening(EventTypes.PLAYER_DIED, OnPlayerDied);
        EventManager.StartListening(EventTypes.PLAYER_IN_POSITION, OnPlayerPositioned);

        EventManager.TriggerEvent(EventTypes.GAME_START);
    }

    void FixedUpdate() {
        if (directionalLight != null) {
            if (fadeOutDelayCounter >= fadeOutDelay) {
                if (!playerFlyInTriggered) {
                    EventManager.TriggerEvent(EventTypes.ENABLE_PLAYER_LIGHT);
                    playerFlyInTriggered = true;
                }
                directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 0f, fadeOutSpeed * Time.deltaTime);
                if (directionalLight.intensity <= 0.5f) {
                    Destroy(directionalLight.gameObject);
                }
            } else {
                fadeOutDelayCounter += Time.deltaTime;
            }
        }
        if (playerFliesInCounter != -1f) {
            if (playerFliesInCounter >= playerFliesInDelay) {
                playerFliesInCounter = -1f;
                EventManager.TriggerEvent(EventTypes.PLAYER_FLY_IN);
            } else {
                playerFliesInCounter += Time.deltaTime;
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

        if (!playerInPosition && playerFliesInCounter == -1f) {
            //camera.transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f,0f,-10f), step);
            camera.transform.Translate(0, 0.05f, 0);
        }

        //start tension
        if (scoreCalculator.playerScore % scoreIntervalTension == 0 && scoreCalculator.playerScore != 0)
        {
            EventManager.TriggerEvent(EventTypes.START_TENSION);
        }

        if (scoreCalculator.playerScore > scoreForPowerups && !powerUpsActive) {
            powerUpsActive = true;
            EventManager.TriggerEvent(EventTypes.ACTIVATE_POWERUPS);
        }
    }

    void OnPlayerPositioned() {
        playerInPosition = true;
        transform.position = new Vector3(0,0,transform.position.z);

        // Reactivate UI
        skillSlider.SetActive(true);
        pauseButton.SetActive(true);
        scorePanel.SetActive(true);
    }

    void OnPlayerDied() {
        playerDied = true;
        pauseButton.SetActive(false);
    }

    void OnGameStart() {
        // reset player model
        SaveLoadController.GetInstance().GetEndlessSession().Reset();

        // hide UI
        skillSlider.SetActive(false);
        pauseButton.SetActive(false);
        scorePanel.SetActive(false);
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
