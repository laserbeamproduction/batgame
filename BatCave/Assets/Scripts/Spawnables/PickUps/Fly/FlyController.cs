using UnityEngine;
using System.Collections;
using System;

public class FlyController : MonoBehaviour {
    public bool markedForDestroy = false;
    
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player")
        {
            EventManager.TriggerEvent(EventTypes.FLY_PICK_UP);
            markedForDestroy = true;
            gameObject.SetActive(false);
        }

        if (col.gameObject.tag == "CleanUp") {
            markedForDestroy = true;
            gameObject.SetActive(false);
        }
    }
}
