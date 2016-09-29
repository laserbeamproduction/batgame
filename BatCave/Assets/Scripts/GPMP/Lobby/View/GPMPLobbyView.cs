using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GPMPLobbyView : MonoBehaviour {

    public GPMPMatchModel matchModel;
    public GameObject errorMessagePanel;
    public Text errorMessageText;
    public Button errorPanelButton;
    public Text errorPanelButtonText;
    public Button toMainMenuButton;

    void Start() {
        EventManager.StartListening(GPMPEvents.Types.GPMP_SHOW_ERROR_MESSAGE.ToString(), OnErrorMessageRecieved);
        if (!GooglePlayHelper.GetInstance().IsPlayerAuthenticated())
            SetMessageForLogin();
    }

    void OnDestroy() {
        EventManager.StopListening(GPMPEvents.Types.GPMP_SHOW_ERROR_MESSAGE.ToString(), OnErrorMessageRecieved);
    }

    private void OnErrorMessageRecieved(object m) {
        string message = (string)m;
        errorMessagePanel.gameObject.SetActive(true);
        toMainMenuButton.gameObject.SetActive(false);
        errorMessageText.text = message;
        errorPanelButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction action = () => { ConfirmMessage(); };
        errorPanelButton.onClick.AddListener(action);
        errorPanelButtonText.text = "OK";
    }

    void SetMessageForLogin() {
        errorMessagePanel.SetActive(true);
        toMainMenuButton.gameObject.SetActive(true);
        errorMessageText.text = "You must be signed in to play online";
        errorPanelButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction action = () => { Login(); };
        errorPanelButton.onClick.AddListener(action);
        errorPanelButtonText.text = "Login";
    }

    public void StartQuickMatch() {
        EventManager.TriggerEvent(GPMPEvents.Types.GPMP_SEARCH_QUICK_MATCH.ToString(), matchModel);
    }

    public void StartWithInvites() {
        EventManager.TriggerEvent(GPMPEvents.Types.GPMP_START_WITH_INVITE.ToString(), matchModel);
    }

    public void ShowAllInvites() {
        EventManager.TriggerEvent(GPMPEvents.Types.GPMP_VIEW_INVITES.ToString());
    }

    public void ToMainMenu() {
        LoadingController.LoadScene(LoadingController.Scenes.MAIN_MENU);
    }

    public void Login() {
        OnErrorMessageRecieved("Click OK if you are logged in");
        GooglePlayHelper.GetInstance().Login();

    }

    public void ConfirmMessage() {
        errorMessagePanel.SetActive(false);
        if (!GooglePlayHelper.GetInstance().IsPlayerAuthenticated())
            SetMessageForLogin();
    }
}
