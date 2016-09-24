using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using System;

public class GPMPGameController : MonoBehaviour {

    string myID = "";
    List<Participant> otherPlayerIDs;

	// Use this for initialization
	void Start () {
    }

    private void OnMatchMakingDone(object arg0) {
        //myID = GPMPLobbyController.GetInstance().GetMyParticipantId();
        //otherPlayerIDs = GPMPLobbyController.GetInstance().GetAllPlayers();
    }

    // Update is called once per frame
    void Update () {
        
	}
}
