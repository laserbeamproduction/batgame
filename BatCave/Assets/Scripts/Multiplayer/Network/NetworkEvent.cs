using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class NetworkEvent : MonoBehaviour {
    public NetworkInstantiator networkInstantiator;
    public LocalInstantiator localInstantiator;

    void OnEnable() {
        //Start Listening to Events
        EventManager.StartListening(EventTypes.START_COUNTDOWN, StartCountdown);
    }

    void OnDisable() {
        //Stop Listening to Events
        EventManager.StopListening(EventTypes.START_COUNTDOWN, StartCountdown);
    }

    private void StartCountdown(object value) {
        //Start Countdown & Instantiate Object Pools
        networkInstantiator.InstantiatePlayerOne();
        localInstantiator.InstantiateLocalObjectPool();
        StartCoroutine(MatchCountDown());
    }

    IEnumerator MatchCountDown() {
        yield return new WaitForSeconds(3);
        EventManager.TriggerEvent(SpawnSystemEvents.TOGGLE_SPAWNING, true);
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
        EventManager.TriggerEvent(EventTypes.HIDE_LOBBY);
    }
    [RPC]
    void StartMatchEvent() {
        EventManager.TriggerEvent(EventTypes.START_MATCH);
    }
}
