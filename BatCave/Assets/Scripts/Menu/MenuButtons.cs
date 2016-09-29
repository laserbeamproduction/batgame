using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {

    void Start() {
        Debug.Log("PlayerRefusedGooglePLay: " + PlayerPrefs.GetInt("PlayerRefusedGooglePLay"));
        if (!PlayerPrefs.HasKey("PlayerRefusedGooglePLay"))
            PlayerPrefs.SetInt("PlayerRefusedGooglePLay", 0);

        if (!Social.localUser.authenticated && !Application.isEditor && PlayerPrefs.GetInt("PlayerRefusedGooglePLay") == 0) {
            GooglePlayHelper.GetInstance().Login();
            AchievementChecker.CheckForWelcomeAchievement();
        }
    }

    public void ShowAchievementsUI() {
        if (!Social.localUser.authenticated && !Application.isEditor) {
            GooglePlayHelper.GetInstance().Login();
        } else {
            GooglePlayHelper.GetInstance().ShowAchievementsUI();
        }
        
    }

    public void ShowLeaderboardUI() {
        if (!Social.localUser.authenticated && !Application.isEditor) {
            GooglePlayHelper.GetInstance().Login();
        } else {
            GooglePlayHelper.GetInstance().ShowLeaderboardUI();
        }
    }

    public void StartEndless()
    {
        LoadingController.LoadScene(LoadingController.Scenes.GAME);
    }

    public void StartArcade()
    {

    }

    public void StartInfoMenu() {
        LoadingController.LoadScene(LoadingController.Scenes.INFO_MENU);
    }

    public void OpenSettings()
    {
        LoadingController.LoadScene(LoadingController.Scenes.OPTION_MENU);
    }

    public void OpenShop()
    {
        LoadingController.LoadScene(LoadingController.Scenes.STORE);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        LoadingController.LoadScene(LoadingController.Scenes.GAME);
    }

    public void MainMenu()
    {
        LoadingController.LoadScene(LoadingController.Scenes.MAIN_MENU);
    }

    public void Multiplayer() {
        LoadingController.LoadScene(LoadingController.Scenes.GPMP_LOBBY);
    }

    public void Leaderboards()
    {

    }
}
