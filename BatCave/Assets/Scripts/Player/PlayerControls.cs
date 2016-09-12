using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
    public GameObject Echo;
    public Transform PlayerPos;
    public PlayerResources playerResources;
    public ScoreCalculator score;
    public SkillSlider skillSlider;
    private Rigidbody2D rigidbody;

    //movement
    private Vector2 movement;
    public float speed = 30;
    private Vector2 fp; // first finger position
    private Vector2 lp; // last finger position
    private float xPosition;
    private bool playerLeft;
    private bool playerRight;

    private bool touchStarted = false;
    private bool isPaused;
    private bool playerIsDead;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        //speed = SaveLoadController.GetInstance().GetOptions().GetControlSensitivity();
        xPosition = rigidbody.position.x;
        EventManager.StartListening(EventTypes.SKILL_VALUE, OnSkillValueRecieved);
        EventManager.StartListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StartListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StartListening(EventTypes.PLAYER_DIED, OnPlayerDied);
    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.SKILL_VALUE, OnSkillValueRecieved);
        EventManager.StopListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StopListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StopListening(EventTypes.PLAYER_DIED, OnPlayerDied);

    }

    void OnGamePaused() {
        isPaused = true;
    }

    void OnGameResume() {
        StartCoroutine(WaitAbit());
    }

    IEnumerator WaitAbit() {
        yield return 1;
        isPaused = false;
    }

    void OnPlayerDied() {
        playerIsDead = true;
    }

    void Update() {
        if (!isPaused && !playerIsDead) {
            CheckPlayerPosition();
            //Swipe Controls
            Vector2 pos = rigidbody.position;
            pos.x = Mathf.MoveTowards(pos.x, xPosition, speed * Time.deltaTime);
            rigidbody.position = pos;
            CheckForSwipe();
            rigidbody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode2D.Force);
        }

        //Motion Contols
        //movement = new Vector2(Input.GetAxis("Horizontal"), 0) * speed; //turn this on for desktop controls
        //movement = new Vector2(Input.acceleration.x, 0) * speed; //turn this on for android controls
    }

    void FixedUpdate()
    {
        if (!isPaused && !playerIsDead) 
            rigidbody.velocity = movement;
    }

    public void SpawnEcho() {
        if (CanAffordEcho(playerResources.echoCost)) {
            EventManager.TriggerEvent(EventTypes.ECHO_USED);
        }
    }

    void OnSkillValueRecieved() {
        GameObject echo = (GameObject)Instantiate(Echo, new Vector3(PlayerPos.position.x, PlayerPos.position.y, -2), Quaternion.identity);
        echo.GetComponent<MoveEcho>().EchoSize(skillSlider.GetLastSkillValue());
    }

    bool CanAffordEcho(float cost) {
        return playerResources.stamina >= cost;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            // Hide player sprite
            GetComponent<SpriteRenderer>().enabled = false;

            // Start blood effect
            GetComponent<ParticleSystem>().Play();

            EventManager.TriggerEvent(EventTypes.PLAYER_DIED);
        }
    }

    void CheckPlayerPosition() {
        playerLeft = false;
        playerRight = false;

        if (xPosition > 1.8)
        {
            playerRight = true;
        }

        else if (xPosition < -1.8)
        {
            playerLeft = true;
        }
    }

    void CheckForSwipe() {
        // DEBUG CODE (Editor debug)
        if (Input.GetMouseButtonUp(0) && Application.isEditor) {
            SpawnEcho();
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if ((fp.x - lp.x) > 5 && !playerLeft && !touchStarted) // left swipe
                {
                    touchStarted = true;
                    xPosition -= 1;
                }
                else if ((fp.x - lp.x) < -5 && !playerRight && !touchStarted) // right swipe
                {
                    touchStarted = true;
                    xPosition += 1;
                }
            }

            if (touch.phase == TouchPhase.Ended) {
                if ((fp.x - lp.x) > 5){
                    touchStarted = false;
                }
                else if ((fp.x - lp.x) < -5){
                    touchStarted = false;
                }
                else if ((fp.x - lp.x) > -3 && fp.y < (Screen.height/2)) {
                    SpawnEcho();
                } else if ((fp.x - lp.x) < 3 && fp.y < (Screen.height / 2)) {
                    SpawnEcho();
                }
            }
        }
    }
}
