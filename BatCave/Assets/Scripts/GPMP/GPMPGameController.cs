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
        EventManager.StartListening(GPMPEvents.GPMP_MATCH_MAKING_DONE, OnMatchMakingDone);
    }

    private void OnMatchMakingDone(object arg0) {
        myID = GPMPController.GetInstance().GetMyParticipantId();
        otherPlayerIDs = GPMPController.GetInstance().GetAllPlayers();
    }

    // Update is called once per frame
    void Update () {
        
	}
}
