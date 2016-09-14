using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

    public string BOOST_ANIMATOR_TRIGGER = "Boost";
    public string FLY_ANIMATOR_TRIGGER = "Fly";

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        EventManager.StartListening(EventTypes.PLAYER_SPEED_PICKUP, OnSpeedBoostPickedUp);
        EventManager.StartListening(EventTypes.PLAYER_SPEED_ENDED, OnSpeedBoostEnded);
    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.PLAYER_SPEED_PICKUP, OnSpeedBoostPickedUp);
        EventManager.StopListening(EventTypes.PLAYER_SPEED_ENDED, OnSpeedBoostEnded);
    }

    void OnSpeedBoostPickedUp() {
        animator.SetTrigger(BOOST_ANIMATOR_TRIGGER);
    }

    void OnSpeedBoostEnded() {
        animator.SetTrigger(FLY_ANIMATOR_TRIGGER);
    }
}
