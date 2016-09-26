using UnityEngine;
using System.Collections;

public class LightDarkControler : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "DarkCollider") {
            EventManager.TriggerEvent(EventTypes.FADE_LIGHT_OUT);
            Debug.Log("Back to the Darkness!");
        }

        if (col.gameObject.tag == "LightCollider") {
            EventManager.TriggerEvent(EventTypes.FADE_LIGHT_IN);
            Debug.Log("It's going to get Bright!");
        }
    }
}
