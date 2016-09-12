using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventManager.StartListening(EventTypes.GAME_OVER, OnGameOver);
        EventManager.StartListening(EventTypes.GAME_START, OnGameStart);

        EventManager.TriggerEvent(EventTypes.GAME_START);
    }

    void OnGameStart() {
        // reset player model
        SaveLoadController.GetInstance().GetEndlessSession().Reset();
    }

    public void PauseGame() {
        EventManager.TriggerEvent(EventTypes.GAME_PAUSED);
    }

    public void ResumeGame() {
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
