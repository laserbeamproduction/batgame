using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleMoveScriptMultiplayer : MonoBehaviour
{
    //Basic MoveScript
    public Vector2 Speed = new Vector2(0.0f, -0.05f);
    private bool isPaused;
    private bool matchStarted = false;

    void Start() {
        EventManager.StartListening(EventTypes.START_MATCH, OnMatchStart);
        EventManager.StartListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StartListening(EventTypes.GAME_RESUME, OnGameResume);
    }

    void OnGamePaused(object value)
    {
        isPaused = true;
    }

    void OnGameResume(object value)
    {
        isPaused = false;
    }

    public void OnMatchStart(object value) {
        matchStarted = true;
    }

    void Update()
    {
        if (!isPaused && matchStarted)
        {
            transform.Translate(Speed.x, Speed.y, 0);
        }
    }
}

