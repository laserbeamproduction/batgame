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
    public float speed;
    private Vector2 fp; // first finger position
    private Vector2 lp; // last finger position
    private float xPosition;
    private bool playerLeft;
    private bool playerRight;

    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        speed = SaveLoadController.GetInstance().GetOptions().GetControlSensitivity();
        xPosition = rigidbody.position.x;
        EventManager.StartListening(EventTypes.SKILL_VALUE, OnSkillValueRecieved);
    }

    void OnDestroy() {
        EventManager.StopListening(EventTypes.SKILL_VALUE, OnSkillValueRecieved);
    }

    void Update () {
        CheckPlayerPosition();
        //Swipe Controls
        Vector2 pos = rigidbody.position;
        pos.x = Mathf.MoveTowards(pos.x, xPosition, speed * Time.deltaTime);
        rigidbody.position = pos;
        CheckForSwipe();
        rigidbody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode2D.Force);

        //Motion Contols
        //movement = new Vector2(Input.GetAxis("Horizontal"), 0) * speed; //turn this on for desktop controls
        //movement = new Vector2(Input.acceleration.x, 0) * speed; //turn this on for android controls

        if (Input.GetMouseButtonDown(0)) {
            SpawnEcho();
        }
    }

    void FixedUpdate()
    {
        rigidbody.velocity = movement;
    }

    public void SpawnEcho() {
<<<<<<< HEAD
        if (!coolingDown)
        {
=======
        if (CanAffordEcho(playerResources.echoCost)) {
>>>>>>> b934439d2aead7c08ab6bed26bd212653a93946a
            EventManager.TriggerEvent(EventTypes.ECHO_USED);
        }
    }

    void OnSkillValueRecieved() {
        GameObject echo = (GameObject)Instantiate(Echo, new Vector3(PlayerPos.position.x, PlayerPos.position.y, -2), Quaternion.identity);
        //echo.GetComponent<MoveEcho>().SetValue(skillSlider.GetLastSkillValue());
    }

    bool CanAffordEcho(float cost) {
        return playerResources.stamina >= cost;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            PlayerPrefs.SetFloat("playerScore", score.playerScore);
            LoadingController.LoadScene(LoadingController.Scenes.GAME_OVER);
            EventManager.TriggerEvent(EventTypes.GAME_OVER);
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
            if (touch.phase == TouchPhase.Ended)
            {
                if ((fp.x - lp.x) > 80 && !playerLeft) // left swipe
                {
                    xPosition -= 1;
                }
                else if ((fp.x - lp.x) < -80 && !playerRight) // right swipe
                {
                    xPosition += 1;
                }
                else if ((fp.x - lp.x) > 80) {
                    //Hacky fix to avoid echo when player is all the way left/rigth
                    // do nothing
                }
                else if ((fp.x - lp.x) < -80) {
                    //do nothing
                }
                else if ((fp.x - lp.x) > -40) {
                    SpawnEcho();
                }
                else if ((fp.x - lp.x) < 40) {
                    SpawnEcho();
                }
            }
        }
    }
}
