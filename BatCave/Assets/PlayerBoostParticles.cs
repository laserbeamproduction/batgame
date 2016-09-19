using UnityEngine;

public class PlayerBoostParticles : MonoBehaviour {

    private ParticleSystem particleSystem;

	void Start () {
        particleSystem = GetComponent<ParticleSystem>();
        EventManager.StartListening(EventTypes.PLAYER_SPEED_ENDED, OnSpeedBoostEnded);
        EventManager.StartListening(EventTypes.PLAYER_SPEED_PICKUP, OnSpeedBoostPickedUp);
    }

    void OnDestroy () {
        EventManager.StopListening(EventTypes.PLAYER_SPEED_ENDED, OnSpeedBoostEnded);
        EventManager.StopListening(EventTypes.PLAYER_SPEED_PICKUP, OnSpeedBoostPickedUp);
    }

    private void OnSpeedBoostPickedUp(object arg0)
    {
        particleSystem.enableEmission = true;
        particleSystem.loop = true;
        particleSystem.Play();
    }

    private void OnSpeedBoostEnded(object arg0)
    {
        particleSystem.enableEmission = false;
        particleSystem.loop = false;
        particleSystem.Stop();
    }
}
