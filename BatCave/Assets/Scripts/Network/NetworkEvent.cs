using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class NetworkEvent : MonoBehaviour {
    public NetworkInstantiator networkInstantiator;

    void OnEnable() {
        //Start Listening to Events
        EventManager.StartListening(EventTypes.INSTANTIATE_OBJECT_POOL, InstantiateNetworkObjects);
    }

    void OnDisable() {
        //Stop Listening to Events
        EventManager.StopListening(EventTypes.INSTANTIATE_OBJECT_POOL, InstantiateNetworkObjects);
    }

    void InstantiateNetworkObjects() {
        networkInstantiator.CanStartSpawning();
    }
}
