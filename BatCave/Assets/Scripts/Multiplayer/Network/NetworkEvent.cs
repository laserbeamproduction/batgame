using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class NetworkEvent : MonoBehaviour {
    public NetworkInstantiator networkInstantiator;
    public LocalInstantiator localInstantiator;

    private int playerCount = 0;

    private bool playersReady = false;

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

    void FixedUpdate() {
        if (playersReady) {
            EventManager.TriggerEvent(EventTypes.START_COUNTDOWN);
            playersReady = false;
        }
    }

    private void ServerStarted(object value)
    {
        networkInstantiator.InstantiatePlayerOne();
    }

    private void PlayerJoinedServer(object value) {
        networkInstantiator.InstantiatePlayerTwo();
    }

    private void StartCountdown(object value) {
        //Start Countdown & Instantiate Object Pools
        if (Network.isServer)
        {
            networkInstantiator.InstantiateNetworkObjectPool();
        }

        localInstantiator.InstantiateLocalObjectPool();

        EventManager.TriggerEvent(EventTypes.START_MATCH);
    }
}
