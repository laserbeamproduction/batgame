using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private Rigidbody2D rigidbody;
    private Animator animator;
    private Vector2 movement;
    public float speed = 30;
    private Vector2 fp; // first finger position
    private Vector2 lp; // last finger position
    private float xPosition;
    private bool playerLeft;
    private bool playerRight;
    private bool touchStarted = false;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<NetworkView>().isMine)
        {
            CheckPlayerPosition();
            //Swipe Controls
            Vector2 pos = rigidbody.position;
            pos.x = Mathf.MoveTowards(pos.x, xPosition, speed * Time.deltaTime);
            rigidbody.position = pos;
            SwipeControls();
            rigidbody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode2D.Force);
        }
    }

    void CheckPlayerPosition()
    {
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

    void SwipeControls() {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
            {
                //SpawnEcho();
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                playerLeft = true;
                xPosition -= 1;
            }
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                playerRight = true;
                xPosition += 1;
            }

            if (Input.GetKeyUp(KeyCode.P))
            {
                animator.SetBool("isBoosting", true);
            }

            if (Input.GetKeyUp(KeyCode.O))
            {
                animator.SetBool("isBoosting", false);
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
                if ((fp.x - lp.x) > 10 && !playerLeft && !touchStarted) // left swipe
                {
                    touchStarted = true;
                    xPosition -= 1;
                }
                else if ((fp.x - lp.x) < -10 && !playerRight && !touchStarted) // right swipe
                {
                    touchStarted = true;
                    xPosition += 1;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if ((fp.x - lp.x) > 10)
                {
                    touchStarted = false;
                }
                else if ((fp.x - lp.x) < -10)
                {
                    touchStarted = false;
                }
                else if ((fp.x - lp.x) > -3 && fp.y < (Screen.height - Screen.height / 4))
                {
                    //SpawnEcho();
                }
                else if ((fp.x - lp.x) < 3 && fp.y < (Screen.height - Screen.height / 4))
                {
                    //SpawnEcho();
                }
            }
        }
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        Vector3 syncPosition = Vector3.zero;
        if (stream.isWriting)
        {
            syncPosition = rigidbody.position;
            stream.Serialize(ref syncPosition);
        }
        else
        {
            stream.Serialize(ref syncPosition);
            rigidbody.position = syncPosition;
        }
    }
}
