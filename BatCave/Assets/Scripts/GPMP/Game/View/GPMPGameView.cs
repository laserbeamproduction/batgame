using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;

public class GPMPGameView : MonoBehaviour {

    public Text participantsTextField;

    void Start() {
        List<Participant> participants = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
        foreach (Participant p in participants) {
            participantsTextField.text += p.DisplayName + "\n";
        }
    }

    public void LeaveGame() {
        EventManager.TriggerEvent(GPMPEvents.GPMP_LEAVE_GAME);
    }
}
