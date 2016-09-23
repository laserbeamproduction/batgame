using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System;
using System.Collections.Generic;

public class GPMPController : RealTimeMultiplayerListener {

    private uint minimumOpponents = 1;
    private uint maximumOpponents = 1;
    private uint gameVariation = 0;

    public string messagelog;

    private static GPMPController instance;

    public static GPMPController GetInstance() {
        if (instance == null)
            instance = new GPMPController();
        return instance;
    }

    public void StartMatchMaking() {
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(minimumOpponents, maximumOpponents, gameVariation, this);
        PlayGamesPlatform.Instance.RealTime.ShowWaitingRoomUI();
    }

    public void StartWithInvitation() {
        PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(minimumOpponents, maximumOpponents, gameVariation, this);
    }

    public void OnRoomSetupProgress(float percent) {
        ShowMPStatus("We are " + percent + "% done with setup");
    }

    public void LeaveRoom() {
        PlayGamesPlatform.Instance.RealTime.LeaveRoom();
    }

    public void OnRoomConnected(bool success) {
        if (success) {
            ShowMPStatus("We are connected to the room! I would probably start our game now.");
            EventManager.TriggerEvent(GPMPEvents.GPMP_MATCH_MAKING_DONE);
        } else {
            ShowMPStatus("Uh-oh. Encountered some error connecting to the room.");
        }
    }

    public void OnLeftRoom() {
        ShowMPStatus("We have left the room. We should probably perform some clean-up tasks.");
    }

    public void OnParticipantLeft(Participant participant) {
        ShowMPStatus("Player " + participant.DisplayName + " has left.");
    }

    public void OnPeersConnected(string[] participantIds) {
        foreach (string participantID in participantIds) {
            ShowMPStatus("Player " + participantID + " has joined.");
        }
    }

    public void OnPeersDisconnected(string[] participantIds) {
        foreach (string participantID in participantIds) {
            ShowMPStatus("Player " + participantID + " has left.");
        }
    }

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {
        ShowMPStatus("We have received some gameplay messages from participant ID:" + senderId);
    }

    private void ShowMPStatus(string message) {
        Debug.Log(message);
        messagelog += message + "\n";
    }

    public List<Participant> GetAllPlayers() {
        return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
    }

    public string GetMyParticipantId() {
        return PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId;
    }
}
