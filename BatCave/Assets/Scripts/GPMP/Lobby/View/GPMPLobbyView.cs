using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GPMPLobbyView : MonoBehaviour {

    public GPMPMatchModel matchModel;

    void Start() {
        GooglePlayHelper.GetInstance().Login();
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
}
