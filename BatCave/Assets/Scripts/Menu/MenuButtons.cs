﻿using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {



	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void ShowAchievementsUI() {
        GooglePlayHelper.GetInstance().ShowAchievementsUI();
    }

    public void ShowLeaderboardUI() {
        GooglePlayHelper.GetInstance().ShowLeaderboardUI();
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

    public void Leaderboards()
    {

    }
}
