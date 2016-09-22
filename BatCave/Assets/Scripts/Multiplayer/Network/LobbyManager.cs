using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour {
    public Button playOnlineButton;
    public GameObject lobbyPanel;
    public Text lfpText;

	void Start () {
        EventManager.StartListening(EventTypes.PLAY_ONLINE_PRESSED, DisableButton);
        EventManager.StartListening(EventTypes.HIDE_LOBBY, HidePanel);

        if (PlayerPrefs.GetString("FailedToFindPlayer") == "true") {
            PlayerPrefs.SetString("FailedToFindPlayer", "false");
            EventManager.TriggerEvent(EventTypes.RESTART_SEARCH);
        }
	}

    private void DisableButton(object value) {
        playOnlineButton.interactable = false;
        lfpText.enabled = true;
    }

    private void HidePanel(object value) {
        lobbyPanel.SetActive(false);
    }
}