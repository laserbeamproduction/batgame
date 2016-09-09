using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventManager.StartListening(EventTypes.GAME_OVER, OnGameOver);
        EventManager.StartListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StartListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StartListening(EventTypes.GAME_START, OnGameStart);

        EventManager.TriggerEvent(EventTypes.GAME_START);
    }

    void OnGameStart() {
        // reset player model
        SaveLoadController.GetInstance().GetEndlessSession().Reset();
    }

    void OnGamePaused() {

    }

    void OnGameResume() {

    }

    void OnGameOver() {
        PlayerSave player = SaveLoadController.GetInstance().GetPlayer();
        EndlessSessionSave gameSession = SaveLoadController.GetInstance().GetEndlessSession();
        GooglePlayHelper gph = GooglePlayHelper.GetInstance();
        player.AddTotalGamesPlayed(1);

        // report events
        gph.ReportEvent(GPGSConstant.event_amount_of_endless_games_started, 1);
        gph.ReportEvent(GPGSConstant.event_score_endless_mode, gameSession.GetScore());
        gph.ReportEvent(GPGSConstant.event_amount_of_flies_gathered_endless, gameSession.GetResourcesGathered());

        // save current stats
        gph.SaveGame(); // TODO: keep track of timeplayed

        // check for achievements
        AchievementChecker.CheckForEndlessScoreAchievement(gameSession.GetScore());

        // highscore post
        if (gameSession.GetScore() > player.GetHighscore()) {
            EventManager.TriggerEvent(EventTypes.NEW_HIGHSCORE);
            player.SetHighscore(gameSession.GetScore());
            gph.PostHighscore(player.GetHighscore(), GPGSConstant.leaderboard_endless_mode);
        }
        
        // start game over screen
        LoadingController.LoadScene(LoadingController.Scenes.GAME_OVER);

    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.GAME_OVER, OnGameOver);
        EventManager.StopListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StopListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StopListening(EventTypes.GAME_START, OnGameStart);
    }
}
