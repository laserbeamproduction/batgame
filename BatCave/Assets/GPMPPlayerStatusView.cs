﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GPMPPlayerStatusView : MonoBehaviour {

    public GameObject waitingForMatchStartPanel;

    public Text playerStatusTextField;
    public Text opponentStatusTextField;

    public Color32 connectedColor;
    public Color32 disconnectedColor;

    private GPMPMatchModel matchModel;

    void Start () {
        EventManager.StartListening(GPMPEvents.Types.GPMP_PLAYER_READY.ToString(), OnPlayerReady); 
        EventManager.StartListening(GPMPEvents.Types.GPMP_OPPONENT_READY.ToString(), OnOpponentReady);
        EventManager.StartListening(GPMPEvents.Types.GPMP_OPPONENT_LEFT.ToString(), OnOpponentLeft);
        EventManager.StartListening(GPMPEvents.Types.GPMP_MATCH_INFO_READY.ToString(), OnMatchInfoReady);
        EventManager.StartListening(GPMPEvents.Types.GPMP_START_GAME.ToString(), OnMatchStarted);
    }

    private void OnMatchInfoReady(object model) {
        matchModel = (GPMPMatchModel)model;

        DebugMP.Log("Player names: " + matchModel.player.DisplayName + " VS " + matchModel.opponent.DisplayName);

        playerStatusTextField.text = "1P: " + matchModel.player.DisplayName;
        //playerStatusTextField.color = disconnectedColor;

        opponentStatusTextField.text = "2P: " + matchModel.opponent.DisplayName;
       // opponentStatusTextField.color = disconnectedColor;
    }

    void OnDestroy() {
        EventManager.StopListening(GPMPEvents.Types.GPMP_PLAYER_READY.ToString(), OnPlayerReady);
        EventManager.StopListening(GPMPEvents.Types.GPMP_OPPONENT_READY.ToString(), OnOpponentReady);
        EventManager.StopListening(GPMPEvents.Types.GPMP_OPPONENT_LEFT.ToString(), OnOpponentLeft);
        EventManager.StopListening(GPMPEvents.Types.GPMP_MATCH_INFO_READY.ToString(), OnMatchInfoReady);
        EventManager.StopListening(GPMPEvents.Types.GPMP_START_GAME.ToString(), OnMatchStarted);
    }

    private void OnMatchStarted(object model) {
        waitingForMatchStartPanel.SetActive(false);
    }

    private void OnOpponentLeft(object arg0) {
       // opponentStatusTextField.color = disconnectedColor;
    }

    private void OnPlayerReady(object arg0) {
       // playerStatusTextField.color = connectedColor;
    }

    private void OnOpponentReady(object arg0) {
      //  opponentStatusTextField.color = connectedColor;
    }
}