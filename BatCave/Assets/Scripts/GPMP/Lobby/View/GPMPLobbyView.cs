using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GPMPLobbyView : MonoBehaviour {

    public GPMPMatchModel matchModel;
    public ScrollRect scrollView;

    void Start() {
        EventManager.StartListening(GPMPEvents.GPMP_TOGGLE_MENU_LOBBY, OnShow);
    }

    void OnDestroy() {
        EventManager.StopListening(GPMPEvents.GPMP_TOGGLE_MENU_LOBBY, OnShow);
    }

    void OnShow(object e) {
        bool enable = (bool)e;
        if (enable)
            scrollView.horizontalNormalizedPosition = 0f;
    }

    public void Login() {
        GooglePlayHelper.GetInstance().Login();
    }

    public void StartQuickMatch() {
        EventManager.TriggerEvent(GPMPEvents.GPMP_SEARCH_QUICK_MATCH, matchModel);
    }

    public void StartWithInvites() {
        EventManager.TriggerEvent(GPMPEvents.GPMP_START_WITH_INVITE, matchModel);
    }

    public void ToMainMenu() {
        LoadingController.LoadScene(LoadingController.Scenes.MAIN_MENU);
    }

}
