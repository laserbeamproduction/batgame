using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
    public GameObject Echo;
    public Transform PlayerPos;
    public float playerYposition;
    public PlayerResources playerResources;
    public ScoreCalculator score;
    public SkillSlider skillSlider;
    public GameObject[] playerEchos;
    public Light playerLight;
    public float playerScaleUpSpeed;
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
    private bool controlsEnabled;
    private bool playerIsFlyingIn;
    private bool lightIsFadingIn;
    private bool lightIsFadingOut;

    private bool canDie = true;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        //speed = SaveLoadController.GetInstance().GetOptions().GetControlSensitivity();
        xPosition = rigidbody.position.x;
        EventManager.StartListening(EventTypes.SKILL_VALUE, OnSkillValueRecieved);
        EventManager.StartListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StartListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StartListening(EventTypes.PLAYER_DIED, OnPlayerDied);
        EventManager.StartListening(EventTypes.PLAYER_FLY_IN, OnPlayerFliesIn);
        EventManager.StartListening(EventTypes.ENABLE_PLAYER_LIGHT, OnPlayerLightEnabled);
        EventManager.StartListening(EventTypes.DISABLE_PLAYER_LIGHT, OnPlayerLightDisabled);
    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.SKILL_VALUE, OnSkillValueRecieved);
        EventManager.StopListening(EventTypes.GAME_RESUME, OnGameResume);
        EventManager.StopListening(EventTypes.GAME_PAUSED, OnGamePaused);
        EventManager.StopListening(EventTypes.PLAYER_DIED, OnPlayerDied);
        EventManager.StopListening(EventTypes.PLAYER_FLY_IN, OnPlayerFliesIn);
        EventManager.StopListening(EventTypes.ENABLE_PLAYER_LIGHT, OnPlayerLightEnabled);
        EventManager.StopListening(EventTypes.DISABLE_PLAYER_LIGHT, OnPlayerLightDisabled);
    }

    void OnGamePaused() {
        isPaused = true;
    }

    void OnPlayerFliesIn() {
        playerIsFlyingIn = true;
    }

    void OnGameResume() {
        StartCoroutine(WaitAbit());
    }

    void OnPlayerLightEnabled() {
        lightIsFadingIn = true;
        lightIsFadingOut = false;
    }

    void OnPlayerLightDisabled() {
        lightIsFadingIn = false;
        lightIsFadingOut = true;
    }

    IEnumerator WaitAbit() {
        yield return 1;
        isPaused = false;
    }

    void OnPlayerDied() {
        playerIsDead = true;
    }

    void Update() {
        if (!isPaused && !playerIsDead && controlsEnabled)
        {
            CheckPlayerPosition();
            //Swipe Controls
            Vector2 pos = rigidbody.position;
            pos.x = Mathf.MoveTowards(pos.x, xPosition, speed * Time.deltaTime);
            rigidbody.position = pos;
            CheckForSwipe();
            rigidbody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode2D.Force);
        }

        rigidbody.velocity = movement;
        if (playerIsFlyingIn)
        {
            if (transform.localScale.x < 1 || transform.localScale.y < 1) {
                transform.localScale += new Vector3(playerScaleUpSpeed, playerScaleUpSpeed, playerScaleUpSpeed);
            } else {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            Vector2 pos = rigidbody.position;
            //pos = Vector2.MoveTowards(pos, new Vector2(pos.x, playerYposition), 5f * Time.deltaTime);
            //rigidbody.position = pos;
            //rigidbody.MovePosition(pos - new Vector2(pos.x, playerYposition) * (2.25f * Time.deltaTime));
            transform.Translate(0, 0.05f, 0);

            if (pos.y >= playerYposition)
            {
                pos.y = playerYposition;
                playerIsFlyingIn = false;
                controlsEnabled = true;
                EventManager.TriggerEvent(EventTypes.PLAYER_IN_POSITION);
                rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        //Motion Contols
        //movement = new Vector2(Input.GetAxis("Horizontal"), 0) * speed; //turn this on for desktop controls
        //movement = new Vector2(Input.acceleration.x, 0) * speed; //turn this on for android controls
    }

    void FixedUpdate()
    {
        if (!isPaused && !playerIsDead) {
            if (lightIsFadingIn) {
                playerLight.intensity = Mathf.Lerp(playerLight.intensity, 8f, 0.5f * Time.deltaTime);
                if (playerLight.intensity >= 7f) {
                    lightIsFadingIn = false;
                    playerLight.intensity = 8f;
                }
            }

            if (lightIsFadingOut) {
                playerLight.intensity = Mathf.Lerp(playerLight.intensity, 0f, 0.5f * Time.deltaTime);
                if (playerLight.intensity >= 1f) {
                    lightIsFadingOut = false;
                    playerLight.intensity = 0f;
                }
            }

        }
    }

    public void SpawnEcho() {
        if (CanAffordEcho(playerResources.echoCost)) {
            EventManager.TriggerEvent(EventTypes.ECHO_USED);
        }
    }

    void OnSkillValueRecieved() {
        foreach (GameObject echo in playerEchos)
        {
            if (!echo.activeInHierarchy)
            {
                echo.SetActive(true);
                echo.transform.position = new Vector3(PlayerPos.position.x, PlayerPos.position.y, -2);
                echo.GetComponent<MoveEcho>().EchoSize(skillSlider.GetLastSkillValue());
                return;
            }
        }
    }

    bool CanAffordEcho(float cost) {
        return playerResources.stamina >= cost;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacle" && canDie)
        {
            // Hide player sprite
            GetComponent<SpriteRenderer>().enabled = false;

            // Start blood effect
            GetComponent<ParticleSystem>().Play();

            EventManager.TriggerEvent(EventTypes.PLAYER_DIED);
        }

        if (col.gameObject.tag == "Obstacle" && !canDie) {
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
        if (Application.isEditor) {
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)) {
                SpawnEcho();
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) {
                playerLeft = true;
                xPosition -= 1;
            }
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) {
                playerRight = true;
                xPosition += 1;
            }
            return;
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

    public void SetShield(bool shieldActive) {
        if (shieldActive) {
            canDie = false;
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (!shieldActive) {
            canDie = true;
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
