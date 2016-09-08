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
        SaveLoadController.GetInstance().GetPlayer().SetCurrentSessionScore(0);
        SaveLoadController.GetInstance().GetPlayer().ResetAmountOfFliesGatheredInRun();
    }

    void OnGamePaused() {

    }

    void OnGameResume() {

    }

    void OnGameOver() {
        PlayerSave player = SaveLoadController.GetInstance().GetPlayer();
        GooglePlayHelper gph = GooglePlayHelper.GetInstance();
        player.AddTotalGamesPlayed(1);

        // report events
        gph.ReportEvent(GPGSConstant.event_amount_of_endless_games_started, 1);
        gph.ReportEvent(GPGSConstant.event_score_endless_mode, player.GetCurrentSessionScore());
        gph.ReportEvent(GPGSConstant.event_amount_of_flies_gathered_endless, player.GetAmountOfFliesGatheredInRun());

        // save current stats
        gph.SaveGame(); // TODO: keep track of timeplayed

        // check for achievements
        AchievementChecker.CheckForEndlessScoreAchievement(player.GetCurrentSessionScore());

        // highscore post
        if (player.GetCurrentSessionScore() > player.GetHighscore()) {
            EventManager.TriggerEvent(EventTypes.NEW_HIGHSCORE);
            player.SetHighscore(player.GetCurrentSessionScore());
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
