using UnityEngine;
using System.Collections;
using System;

public class FlyController : MonoBehaviour {
    public bool markedForDestroy = false;
    
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player")
        {
            EventManager.TriggerEvent(EventTypes.FLY_PICK_UP);
            Debug.Log("A fly has been picked up!");
            markedForDestroy = true;
        }

        if (col.gameObject.tag == "CleanUp") {
            markedForDestroy = true;
        }
    }
}
