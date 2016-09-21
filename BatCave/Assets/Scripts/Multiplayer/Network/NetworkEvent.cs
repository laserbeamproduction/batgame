using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class NetworkEvent : MonoBehaviour {
    public NetworkInstantiator networkInstantiator;
    public LocalInstantiator localInstantiator;

    void OnEnable() {
        //Start Listening to Events
        EventManager.StartListening(EventTypes.SERVER_STARTED, ServerStarted);
        EventManager.StartListening(EventTypes.PLAYER_TWO_JOINED, PlayerJoinedServer);
        EventManager.StartListening(EventTypes.START_COUNTDOWN, StartCountdown);
    }

    void OnDisable() {
        //Stop Listening to Events
        EventManager.StopListening(EventTypes.SERVER_STARTED, ServerStarted);
        EventManager.StopListening(EventTypes.PLAYER_TWO_JOINED, PlayerJoinedServer);
        EventManager.StopListening(EventTypes.START_COUNTDOWN, StartCountdown);
    }

    private void ServerStarted(object value)
    {
        networkInstantiator.InstantiatePlayerOne();
        localInstantiator.InstantiateLocalObjectPool();
    }

    private void PlayerJoinedServer(object value)
    {
        networkInstantiator.InstantiatePlayerTwo();
        localInstantiator.InstantiateLocalObjectPool();
    }

    private void StartCountdown(object value) {
        //Start Countdown & Instantiate Object Pools
        if (Network.isServer)
        {
            networkInstantiator.InstantiateNetworkObjectPool();
        }

        StartCoroutine(MatchCountDown());
    }

    IEnumerator MatchCountDown() {
        yield return new WaitForSeconds(3);
        GetComponent<NetworkView>().RPC("StartMatchEvent", RPCMode.All);
        Debug.Log("Match Started");
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        GetComponent<NetworkView>().RPC("CountdownEvent", RPCMode.All);
        Debug.Log("Player Connected to Server");
    }

    [RPC]
    void CountdownEvent() {
        EventManager.TriggerEvent(EventTypes.START_COUNTDOWN);
    }
    [RPC]
    void StartMatchEvent() {
        EventManager.TriggerEvent(EventTypes.START_MATCH);
    }
}
