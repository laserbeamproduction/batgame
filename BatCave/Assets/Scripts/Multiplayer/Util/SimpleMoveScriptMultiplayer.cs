using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleMoveScriptMultiplayer : MonoBehaviour
{
    public Vector2 Speed = new Vector2(0.0f, -0.05f);
    private bool matchStarted = false;

    void Start() {
        EventManager.StartListening(EventTypes.START_MATCH, OnMatchStart);
    }

    public void OnMatchStart(object value) {
        matchStarted = true;
    }

    void Update()
    {
        if (matchStarted)
        {
            transform.Translate(Speed.x, Speed.y, 0);
        }
    }
}

