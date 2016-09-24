using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GPMPWaitingView : MonoBehaviour {

    public Text statusTextField;
    public ScrollRect scrollView;

    void Start() {
        EventManager.StartListening(GPMPEvents.GPMP_TOGGLE_WAITING_LOBBY, OnShow);
        EventManager.StartListening(GPMPEvents.GPMP_REPORT_ROOM_SETUP_PROGRESS, OnProgressStatusUpdate);
    }

    void OnDestroy() {
        EventManager.StopListening(GPMPEvents.GPMP_TOGGLE_WAITING_LOBBY, OnShow);
        EventManager.StopListening(GPMPEvents.GPMP_REPORT_ROOM_SETUP_PROGRESS, OnProgressStatusUpdate);
    }

    public void CancelMatching() {
        EventManager.TriggerEvent(GPMPEvents.GPMP_CANCEL_MATCH_MAKING);
        EventManager.TriggerEvent(GPMPEvents.GPMP_TOGGLE_WAITING_LOBBY, false);
        EventManager.TriggerEvent(GPMPEvents.GPMP_TOGGLE_MENU_LOBBY, true);
    }

    void OnProgressStatusUpdate(object percentage) {
        statusTextField.text = (string)percentage;
    }

    void OnShow(object e) {
        bool enable = (bool)e;
        if (enable) 
            scrollView.horizontalNormalizedPosition = 1f;
    }
}
