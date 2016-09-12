using UnityEngine;

public class SimpleMoveScript : MonoBehaviour {
    public Vector2 Speed = new Vector2(0.1f, 0.1f);
    private Rigidbody2D rb;
    private bool isPaused;
    //public float speed;
    //public Vector3 direction;

    void Start() {
        //this.speed = SaveLoadController.GetInstance().GetPlayer().GetSpeed();
        //EventManager.StartListening(EventTypes.PLAYER_SPEED_CHANGED, OnSpeedChanged);
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(Speed.x, Speed.y);
        }
        EventManager.StartListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StartListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StartListening(EventTypes.PLAYER_DIED, OnGamePaused);
    }

    void OnGamePaused() {
        isPaused = true;
    }

    void OnGameResume() {
        isPaused = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        //this.gameObject.transform.Translate(direction * speed * Time.deltaTime);
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

    void OnSpeedChanged() {
        //this.speed = SaveLoadController.GetInstance().GetPlayer().GetSpeed();
    }

    void OnDestroy() {
        //EventManager.StopListening(EventTypes.PLAYER_SPEED_CHANGED, OnSpeedChanged);
        EventManager.StopListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StopListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StopListening(EventTypes.PLAYER_DIED, OnGamePaused);

    }
}
