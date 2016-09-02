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
        GooglePlayGames.PlayGamesPlatform.Activate();
        if (Social.localUser.authenticated) {
            loginButton.gameObject.SetActive(false);
            logoutButton.gameObject.SetActive(true);
        } else {
            loginButton.gameObject.SetActive(true);
            logoutButton.gameObject.SetActive(false);
        }
    }

    public void Login() {
        string message;
        Social.localUser.Authenticate((bool success) => {
            if (success) {
                message = "Welcome " + Social.localUser.userName;
                string token = GooglePlayGames.PlayGamesPlatform.Instance.GetToken();
                loginButton.gameObject.SetActive(false);
                logoutButton.gameObject.SetActive(true);
            } else {
                message = "Login failed!";
            }
            messageField.text = message;
        });
    }

    public void Logout() {
        ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
        loginButton.gameObject.SetActive(true);
        logoutButton.gameObject.SetActive(false);
        messageField.text = "logged out.";
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
