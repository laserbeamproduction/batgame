using UnityEngine;
using System.Collections;
using System;

public class FlyController : MonoBehaviour {
    
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player")
        {
            EventManager.TriggerEvent(EventTypes.FLY_PICK_UP);
            Debug.Log("A fly has been picked up!");
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "CleanUp") {
            Destroy(gameObject);
        }
    }
}
