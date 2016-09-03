using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {

    public Button loginButton;
    public Button logoutButton;
    public Text messageField;

	// Use this for initialization
	void Start () {
        GooglePlayHelper gph = GooglePlayHelper.GetInstance();
        //if (Social.localUser.authenticated) {
            gph.Login();
        //}
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void StartEndless()
    {
        LoadingController.LoadScene(LoadingController.Scenes.GAME);
    }

    public void StartArcade()
    {

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
