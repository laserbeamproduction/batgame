using UnityEngine;
using System.Collections.Generic;

public class LightPowerController : MonoBehaviour {

    private ParticleSystem particleSystem;

    void Start() {
        particleSystem = GetComponent<ParticleSystem>();
        EventManager.StartListening(EventTypes.SET_DAY_TIME, OnLightPowerPickedUp);
    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.SET_DAY_TIME, OnLightPowerPickedUp);
    }

    void OnLightPowerPickedUp(Dictionary<string, object> arg0) {
        particleSystem.Play();
    }
}
