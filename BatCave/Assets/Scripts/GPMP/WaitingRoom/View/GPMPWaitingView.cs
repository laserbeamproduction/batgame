using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GPMPWaitingView : MonoBehaviour {

    public Text statusTextField;

    void Start() {
        EventManager.StartListening(GPMPEvents.Types.GPMP_REPORT_ROOM_SETUP_PROGRESS.ToString(), OnProgressStatusUpdate);
    }

    void OnDestroy() {
        EventManager.StopListening(GPMPEvents.Types.GPMP_REPORT_ROOM_SETUP_PROGRESS.ToString(), OnProgressStatusUpdate);
    }

    public void CancelMatching() {
        EventManager.TriggerEvent(GPMPEvents.Types.GPMP_CANCEL_MATCH_MAKING.ToString());
    }

    void OnProgressStatusUpdate(object percentage) {
        statusTextField.text = ((float)percentage).ToString();
    }
}
