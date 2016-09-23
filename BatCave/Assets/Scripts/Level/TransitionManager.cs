using UnityEngine;
using System.Collections;

public class TransitionManager : MonoBehaviour {
    public Sprite[] transitionOutSprites;
    public Sprite[] transitionInSprites;

	void Start () {
        EventManager.StartListening(EventTypes.TRANSITION_START, StartTransition);
	}

    private void StartTransition(object value) {
        EventManager.TriggerEvent(EventTypes.SET_TRANSITION_OUT_SPRITES, transitionOutSprites);
    }


}
