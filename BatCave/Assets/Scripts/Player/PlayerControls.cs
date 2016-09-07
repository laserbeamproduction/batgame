using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
    public GameObject Echo;
    public Transform PlayerPos;
    public PlayerResources playerResources;
    public ScoreCalculator score;
    private Rigidbody2D rigidbody;

    //movement
    private Vector2 movement;
    public float speed;
    private Vector2 fp; // first finger position
    private Vector2 lp; // last finger position
    private float xPosition;
    private bool playerLeft;
    private bool playerRight;

    //echo
    public float echoCoolDownTime;
    public float currentCoolDownTime;
    private bool coolingDown;

    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        speed = SaveLoadController.GetInstance().GetOptions().GetControlSensitivity();
        xPosition = rigidbody.position.x;
    }
	
	void Update () {
        CheckPlayerPosition();
        //Swipe Controls
        Vector2 pos = rigidbody.position;
        pos.x = Mathf.MoveTowards(pos.x, xPosition, speed * Time.deltaTime);
        rigidbody.position = pos;
        CheckForSwipe();
        rigidbody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode2D.Force);

        //check if echo is cooling down
        checkCoolDown();

        //Motion Contols
        //movement = new Vector2(Input.GetAxis("Horizontal"), 0) * speed; //turn this on for desktop controls
        //movement = new Vector2(Input.acceleration.x, 0) * speed; //turn this on for android controls
    }

    void FixedUpdate()
    {
        rigidbody.velocity = movement;
    }

    public void SpawnEcho() {
        if (!coolingDown && playerResources.stamina > 0)
        {
            EventManager.TriggerEvent(EventTypes.ECHO_USED);
            currentCoolDownTime = echoCoolDownTime;
            Instantiate(Echo, new Vector3(PlayerPos.position.x, PlayerPos.position.y, -2), Quaternion.identity);
        }
    }

    void checkCoolDown()
    {
        if (currentCoolDownTime > 0)
        {
            coolingDown = true;
            currentCoolDownTime -= Time.deltaTime;
        }

        if (currentCoolDownTime <= 0)
        {
            currentCoolDownTime = 0;
            coolingDown = false;
        }
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
