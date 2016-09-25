using UnityEngine;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using System;
using GooglePlayGames;
using System.Collections;

public class GPMPGameController : MonoBehaviour {
    
    byte protocolVersion = 1;
    private GPMPMatchModel matchModel;

    void Start () {
        StartCoroutine(ExecuteAfterTime(0.1f));
        EventManager.StartListening(GPMPEvents.Types.GPMP_UPDATE_MY_POSITION.ToString(), OnMyPositionUpdated);
        EventManager.StartListening(GPMPEvents.Types.GPMP_MESSAGE_RECIEVED.ToString(), OnMessageRecieved);
        EventManager.StartListening(GPMPEvents.Types.GPMP_PLAYER_READY.ToString(), OnPlayerReady);
        EventManager.StartListening(GPMPEvents.Types.GPMP_OPPONENT_READY.ToString(), OnOpponentReady);
    }

    void OnDestroy() {
        EventManager.StopListening(GPMPEvents.Types.GPMP_UPDATE_MY_POSITION.ToString(), OnMyPositionUpdated);
        EventManager.StopListening(GPMPEvents.Types.GPMP_MESSAGE_RECIEVED.ToString(), OnMessageRecieved);
        EventManager.StopListening(GPMPEvents.Types.GPMP_PLAYER_READY.ToString(), OnPlayerReady);
        EventManager.StopListening(GPMPEvents.Types.GPMP_OPPONENT_READY.ToString(), OnOpponentReady);
    }

    IEnumerator ExecuteAfterTime(float time) {
        yield return new WaitForSeconds(time);
        SetParticipantsInfo();
    }

    private static GPMPController instance;

    public static GPMPController GetInstance() {
        if (instance == null)
            instance = new GPMPController();
        return instance;
    }

    void OnMyPositionUpdated(object position) {
        Vector3 pos = (Vector3)position;
        List<byte> m = new List<byte>();
        m.AddRange(BitConverter.GetBytes(pos.x));
        m.AddRange(BitConverter.GetBytes(pos.y));
        m.AddRange(BitConverter.GetBytes(pos.z));

        // For the other player you are the opponent so we pre translate here
        SendMessage(GPMPEvents.Types.GPMP_UPDATE_OPPONENT_POSITION, m);
    }

    void SendMessage(GPMPEvents.Types eventType, List<byte> message) {
        DebugMP.Log("Sending message to opponent version: " + protocolVersion);
        List<byte> m = new List<byte>();
        m.Add(protocolVersion); // first byte is the version
        m.AddRange(BitConverter.GetBytes((int)eventType)); // second to fifth byte is the event
        m.AddRange(message);
        byte[] messageToSend = m.ToArray();
        PlayGamesPlatform.Instance.RealTime.SendMessage(false, matchModel.opponent.ParticipantId, messageToSend);
        protocolVersion++;
    }

    private void OnMessageRecieved(object data) {
        byte[] bytes = (byte[])data;
        byte messageVersion = (byte)bytes[0];
        GPMPEvents.Types command = (GPMPEvents.Types)BitConverter.ToInt32(bytes, 1);
        EventManager.TriggerEvent(command.ToString(), bytes);
        DebugMP.Log("Message recieved from other player. Version: " + (byte)bytes[0]);
    }

    private void OnPlayerReady(object arg0) {
        // Let the other player know you are ready
        SendMessage(GPMPEvents.Types.GPMP_OPPONENT_READY, new List<byte>());
        
        // If our opponent is also ready dispatch start game
    }

    private void OnOpponentReady(object arg0) {
        // If we are also ready dispatch start game

    }

    private void SetParticipantsInfo() {
        matchModel.player = PlayGamesPlatform.Instance.RealTime.GetSelf();
        matchModel.iAmTheHost = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants()[0].ParticipantId == matchModel.player.ParticipantId;
        List<Participant> players = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
        foreach (Participant p in players) {
            if (p.ParticipantId != matchModel.player.ParticipantId)
                matchModel.opponent = p;
        }
        EventManager.TriggerEvent(GPMPEvents.Types.GPMP_MATCH_INFO_READY.ToString(), matchModel);
    }
}
