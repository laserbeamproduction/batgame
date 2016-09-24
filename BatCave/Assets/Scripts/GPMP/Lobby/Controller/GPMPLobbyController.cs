using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections.Generic;
using System;

public class GPMPLobbyController : MonoBehaviour, RealTimeMultiplayerListener {

    void Start() {
        EventManager.StartListening(GPMPEvents.GPMP_SEARCH_QUICK_MATCH, OnStartSearchForQuickMatch);
        EventManager.StartListening(GPMPEvents.GPMP_START_WITH_INVITE, OnStartInvitingForMatch);
        EventManager.StartListening(GPMPEvents.GPMP_CANCEL_MATCH_MAKING, OnMatchMakingCanceled);
        EventManager.StartListening(GPMPEvents.GPMP_LEAVE_GAME, OnPlayerLeavesTheGame);
    }

    void OnDestroy() {
        EventManager.StopListening(GPMPEvents.GPMP_SEARCH_QUICK_MATCH, OnStartSearchForQuickMatch);
        EventManager.StopListening(GPMPEvents.GPMP_START_WITH_INVITE, OnStartInvitingForMatch);
        EventManager.StopListening(GPMPEvents.GPMP_CANCEL_MATCH_MAKING, OnMatchMakingCanceled);
        EventManager.StopListening(GPMPEvents.GPMP_LEAVE_GAME, OnPlayerLeavesTheGame);
    }

    private void OnPlayerLeavesTheGame(object arg0) {
        DebugMP.Log("Player is leaving the room");
        PlayGamesPlatform.Instance.RealTime.LeaveRoom();
        LoadingController.LoadScene(LoadingController.Scenes.GPMP_LOBBY);
    }

    private void OnStartSearchForQuickMatch(object model) {
        DebugMP.Log("Start searching for quick match");
        GPMPMatchModel matchModel = (GPMPMatchModel)model;
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(matchModel.minimumAmountOpponents, matchModel.maximumAmountOpponents, 0, this);
        EventManager.TriggerEvent(GPMPEvents.GPMP_TOGGLE_WAITING_LOBBY, true);
    }

    private void OnStartInvitingForMatch(object model) {
        DebugMP.Log("Start inviting for match");
        GPMPMatchModel matchModel = (GPMPMatchModel)model;
        PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(matchModel.minimumAmountOpponents, matchModel.maximumAmountOpponents, 0, this);
        EventManager.TriggerEvent(GPMPEvents.GPMP_TOGGLE_WAITING_LOBBY, true);
    }

    public void OnRoomSetupProgress(float percentage) {
        DebugMP.Log("Room setup progress: " + percentage);
        EventManager.TriggerEvent(GPMPEvents.GPMP_REPORT_ROOM_SETUP_PROGRESS, percentage);
    }

    public void OnMatchMakingCanceled(object obj) {
        DebugMP.Log("Player canceled match making");
        PlayGamesPlatform.Instance.RealTime.LeaveRoom();
    }

    public void OnRoomConnected(bool success) {
        if (success) {
            DebugMP.Log("Room connected. Start loading game scene");
            EventManager.TriggerEvent(GPMPEvents.GPMP_MATCH_MAKING_DONE);
            LoadingController.LoadScene(LoadingController.Scenes.GPMP_GAME);
        } else {
            DebugMP.Log("On Room Connected status: " + success);
        }
    }

    public void OnLeftRoom() {
        DebugMP.Log("Player left the room");
    }

    public void OnParticipantLeft(Participant participant) {
        DebugMP.Log("Player " + participant.DisplayName + " has left the room");
    }

    public void OnPeersConnected(string[] participantIds) {
        foreach (string participantID in participantIds) {
            DebugMP.Log("Player " + participantID + " has joined the room");
        }
    }

    public void OnPeersDisconnected(string[] participantIds) {
        foreach (string participantID in participantIds) {
            DebugMP.Log("Player " + participantID + " has left the room");
        }
    }

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {
        DebugMP.Log("We have received some gameplay messages from participant ID:" + senderId);
    }

    public List<Participant> GetAllPlayers() {
        return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
    }

    public string GetMyParticipantId() {
        return PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId;
    }
}
