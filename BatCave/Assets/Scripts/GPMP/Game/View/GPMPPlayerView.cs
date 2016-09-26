using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class GPMPPlayerView : MonoBehaviour {

    public float networkCallDelay;
    public float speed;
    public Vector3 playerOneSpawnpoint;
    public Vector3 playerTwoSpawnpoint;

    private Rigidbody2D rigidbody;
    private bool playerLeft;
    private bool playerRight;
    private float xPosition;
    private Vector2 fp;
    private Vector2 lp;
    private bool touchStarted;
    private float lastNetworkCall;
    private GPMPMatchModel matchModel;

    
    void Start () {
        // Player me should listen to the player and update its position to the other player
        rigidbody = GetComponent<Rigidbody2D>();
        EventManager.StartListening(GPMPEvents.Types.GPMP_MATCH_INFO_READY.ToString(), OnMatchInfoReady);
    }

    void OnDestroy() {
        EventManager.StopListening(GPMPEvents.Types.GPMP_MATCH_INFO_READY.ToString(), OnMatchInfoReady);
    }

    private void OnMatchInfoReady(object model) {
        matchModel = (GPMPMatchModel)model;
        if (matchModel.iAmTheHost) {
            rigidbody.position = playerTwoSpawnpoint;
        } else {
            rigidbody.position = playerOneSpawnpoint;
        }
        xPosition = rigidbody.position.x;
    }
    
    void Update() {
        CheckPlayerPosition();
        //Swipe Controls
        Vector3 pos = rigidbody.position;
        pos.x = Mathf.MoveTowards(pos.x, xPosition, speed * Time.deltaTime);
        rigidbody.position = pos;
        CheckForSwipe();
        rigidbody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode2D.Force);

        if (lastNetworkCall >= networkCallDelay) {
            lastNetworkCall = 0;
            EventManager.TriggerEvent(GPMPEvents.Types.GPMP_UPDATE_MY_POSITION.ToString(), transform.position);
        } else {
            lastNetworkCall += Time.deltaTime;
        }
    }

    void CheckPlayerPosition() {
        playerLeft = false;
        playerRight = false;

        if (xPosition > 1.8) {
            playerRight = true;
        } else if (xPosition < -1.8) {
            playerLeft = true;
        }
    }

    void CheckForSwipe() {
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                fp = touch.position;
                lp = touch.position;
            }
            if (touch.phase == TouchPhase.Moved) {
                lp = touch.position;
            }
            if (touch.phase == TouchPhase.Moved) {
                if ((fp.x - lp.x) > 10 && !playerLeft && !touchStarted) // left swipe
                {
                    touchStarted = true;
                    xPosition -= 1;
                } else if ((fp.x - lp.x) < -10 && !playerRight && !touchStarted) // right swipe
                  {
                    touchStarted = true;
                    xPosition += 1;
                }
            }

            if (touch.phase == TouchPhase.Ended) {
                if ((fp.x - lp.x) > 10) {
                    touchStarted = false;
                } else if ((fp.x - lp.x) < -10) {
                    touchStarted = false;
                } else if ((fp.x - lp.x) > -3 && fp.y < (Screen.height - Screen.height / 4)) {
                    //SpawnEcho();
                } else if ((fp.x - lp.x) < 3 && fp.y < (Screen.height - Screen.height / 4)) {
                    //SpawnEcho();
                }
            }
        }
    }

}
