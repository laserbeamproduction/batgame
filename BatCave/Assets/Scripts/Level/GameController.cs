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
    }

    void OnGamePaused() {

    }

    void OnGameResume() {

    }

    void OnGameOver() {
        PlayerSave player = SaveLoadController.GetInstance().GetPlayer();
        player.AddTotalGamesPlayed(1);

        // save current stats
        byte[] saveGame = SaveLoadController.GetInstance().CreateSaveObject();
        GooglePlayHelper.GetInstance().SaveGame(saveGame, new System.TimeSpan(0,0,0)); // TODO: keep track of timeplayed

        // check for achievements
        AchievementChecker.CheckForEndlessScoreAchievement(player.GetCurrentSessionScore());

        // highscore post
        if (player.GetCurrentSessionScore() > player.GetHighscore()) {
            EventManager.TriggerEvent(EventTypes.NEW_HIGHSCORE);
            player.SetHighscore(player.GetCurrentSessionScore());
            GooglePlayHelper.GetInstance().PostHighscore(player.GetHighscore(), GPGSConstant.leaderboard_endless_mode);
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
