using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleMoveScript : MonoBehaviour {
    public float startSpeed;
    public Vector2 Speed = new Vector2(0.0f, -0.05f);
    private Rigidbody2D rb;
    private bool isPaused;
    //public float speed;
    //public Vector3 direction;
    public float speedIncreaseTime;
    public float maxSpeedIncrease;
    public int amountOfSpeedIncrements = 4;
    private bool increaseSpeed = true;

    void Start() {
        Speed = new Vector2(0, startSpeed);
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(Speed.x, Speed.y);
        }
        EventManager.StartListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StartListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StartListening(EventTypes.PLAYER_DIED, OnGamePaused);

        StartCoroutine(StartTimer());
    }

    void OnGamePaused() {
        isPaused = true;
    }

    void OnGameResume() {
        isPaused = false;
    }

    void Update() {
        if (!isPaused) {
            if (rb == null) {
                transform.Translate(Speed.x, Speed.y, 0);
            } else {
                transform.Translate(Speed.x, Speed.y, 0);
            }
        }
    }

    void OnDestroy() {
        //EventManager.StopListening(EventTypes.PLAYER_SPEED_CHANGED, OnSpeedChanged);
        EventManager.StopListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StopListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StopListening(EventTypes.PLAYER_DIED, OnGamePaused);

    }

    IEnumerator StartTimer() {
        //speedIncreaseTime -= Time.deltaTime;
        while (increaseSpeed) {
            yield return new WaitForSeconds(speedIncreaseTime / amountOfSpeedIncrements);

            Speed = new Vector2(0, Speed.y - (maxSpeedIncrease/amountOfSpeedIncrements));

            if (Speed.y >= (startSpeed + maxSpeedIncrease))
            {
                increaseSpeed = false;
            }
        }
    }
}
